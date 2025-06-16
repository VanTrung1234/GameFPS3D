using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    
    public bool isActiveWeapon;
    public int weaponDamage;
    
    public bool isShooting, readytoShoot; // Trạng thái bắn (isShooting) và trạng thái sẵn sàng bắn (readytoShoot)
    bool allowReset = true; // Biến để kiểm soát việc reset vũ khí
    public float shootingDelay = 2f; // Thời gian trễ giữa các lần bắn

    public int bulletsPerBurst = 3; // Số viên đạn trong một đợt bắn Burst
    public int BurstBulletLeft; // Số viên đạn còn lại trong chế độ Burst
    internal bool hasTriggeredRecoil = false;


    protected float spreadIntensity=0; // Độ lan tỏa của viên đạn

    public float hipSpreadIntensity; // Độ lan tỏa của viên đạn
    public float adsSpreadIntensity; // Độ lan tỏa của viên đạn




    public GameObject bulletPrefab; // Prefab cho viên đạn
    public Transform bulletSpawn; // Vị trí xuất phát của viên đạn
    public float bulletVolocity = 30; // Tốc độ bay của viên đạn
    public float bulletPrefabLifeTime = 3f; // Thời gian tồn tại của viên đạn

    public GameObject muzzleEffect;

    internal Animator animator;

    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReload = false;

    internal int sodan;

    internal bool isADS;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;
    public enum WeaponModel
    {
        M1911,
        M4_8,
        Bennelli_M4
    }
    public WeaponModel weaponmodel;
    // Enum định nghĩa các chế độ bắn
    public enum ShootingMode
    {
        Single, // Bắn đơn
        Burst, // Bắn liên tiếp (Burst)
        Auto // Bắn tự động (Auto)
    }

    public ShootingMode shootingModeCurrent; // Chế độ bắn hiện tại

    // Phương thức khởi tạo ban đầu
    private void Awake()
    {

        readytoShoot = true; // Đặt vũ khí sẵn sàng để bắn
        spreadIntensity = hipSpreadIntensity;
        BurstBulletLeft = bulletsPerBurst; // Khởi tạo số viên đạn trong Burst
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        sodan = magazineSize;
        
        
        
    }

    // Update được gọi mỗi frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("EnterADS");
            
            isADS = true;
            HUDManager.instance.middleDot.SetActive(false);
            spreadIntensity = adsSpreadIntensity;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetTrigger("ExitADS");
            isADS = false;
            HUDManager.instance.middleDot.SetActive(true);
            spreadIntensity = hipSpreadIntensity;
        }

        if (isActiveWeapon)
        {


            transform.gameObject.layer = LayerMask.NameToLayer("RenderWeapon");
            foreach (Transform chil in transform)
            {
                chil.gameObject.layer = LayerMask.NameToLayer("RenderWeapon");
               
            }


            GetComponent<Outline>().enabled = false;



            // Kiểm tra chế độ bắn và gán giá trị cho isShooting
            if (shootingModeCurrent == ShootingMode.Auto)
            {
                // Bắn liên tục khi nhấn và giữ chuột trái (Auto mode)
                isShooting = Input.GetKey(KeyCode.Mouse0);
                hasTriggeredRecoil = false;
            }
            else if (shootingModeCurrent == ShootingMode.Single ||
                shootingModeCurrent == ShootingMode.Burst)
            {
                // Bắn một lần khi nhấn chuột trái (Single/Burst mode)
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
                hasTriggeredRecoil = false;
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReload == false && WeaponManage.Instance.checkAmmoLeftFor(weaponmodel) > 0)
            {

                Reload();

            }


            // Nếu vũ khí sẵn sàng và người chơi đang bắn
            if (readytoShoot && isShooting && bulletsLeft > 0 && bulletsLeft >= bulletsPerBurst)
            {
                BurstBulletLeft = bulletsPerBurst; // Khởi tạo lại số viên đạn trong chế độ Burst

                FireWeapon(); // Thực hiện bắn vũ khí

            }
            if (bulletsLeft < bulletsPerBurst && isShooting)
            {


                SoundManage.instance.magazineSourcem1911.Play();


            }

        }
        else
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
            foreach (Transform chil in transform)
            {
                chil.gameObject.layer = LayerMask.NameToLayer("Default");
                
            }
        }
        
    }

    

    // Phương thức để bắn vũ khí
    private void FireWeapon()
    {

       animator.enabled = true;
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        //animator.SetTrigger("RECOIL");
        if (!hasTriggeredRecoil)
        {
            if (isADS)
            {
                
                animator.SetTrigger("RECOIL_ADS");
            }
            else
            {

                animator.SetTrigger("RECOIL");
            }

            if (bulletsPerBurst >= 2)
            {
                
            }
            else
            {
                
            }

            hasTriggeredRecoil = true;
        }
        
        //SoundManage.instance.shootingSourcem1911.Play();
        SoundManage.instance.PlayShootingSound(weaponmodel);
        readytoShoot = false; // Đặt vũ khí không sẵn sàng để bắn

        // Tính toán hướng bắn và độ lan tỏa
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Tạo viên đạn mới và thiết lập vị trí, hướng
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul=bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        bullet.transform.forward = shootingDirection; // Đặt hướng của viên đạn

        // Áp dụng lực vào viên đạn để di chuyển
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVolocity, ForceMode.Impulse);

        // Thực hiện Coroutine để hủy viên đạn sau một khoảng thời gian
        StartCoroutine(DeytroyBulletAftterTime(bullet, bulletPrefabLifeTime));

        // Kiểm tra và thực hiện reset sau một khoảng thời gian
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay); // Gọi ResetShot sau thời gian delay
            allowReset = false; // Đặt allowReset thành false để tránh gọi lại
        }

        // Nếu chế độ Burst và còn đạn, tiếp tục bắn
        if (shootingModeCurrent == ShootingMode.Burst && BurstBulletLeft > 1 )
        {
            
            BurstBulletLeft--; // Giảm số viên đạn còn lại
            Invoke("FireWeapon", shootingDelay); // Gọi lại FireWeapon sau delay}
            animator.enabled = false;
        }
        

    }
    private void Reload()
    {
        
        animator.SetTrigger("RELOAD");
        //SoundManage.instance.reloadSourcem1911.Play();
        SoundManage.instance.PlayReloadSound(weaponmodel);
        isReload = true;
        Invoke("ReloadCompleted",reloadTime);
        
    }

    private void ReloadCompleted()
    {
        if (WeaponManage.Instance.checkAmmoLeftFor(weaponmodel) > magazineSize || WeaponManage.Instance.total>=magazineSize)
        {
            
            WeaponManage.Instance.DecreaseTotalAmmo(bulletsLeft, weaponmodel,magazineSize);
           
            bulletsLeft += WeaponManage.Instance.tepm;
        }
        if(WeaponManage.Instance.total<magazineSize) 
        {
            
            bulletsLeft = WeaponManage.Instance.total;
            WeaponManage.Instance.DecreaseTotalAmmo(bulletsLeft, weaponmodel,magazineSize);
        }
        isReload = false;
        
    }

    // Phương thức để reset trạng thái vũ khí sau khi bắn
    private void ResetShot()
    {
        readytoShoot = true; // Đặt vũ khí sẵn sàng để bắn
        allowReset = true; // Cho phép reset lại
    }

    // Phương thức tính toán hướng bắn và độ lan tỏa của viên đạn
    public Vector3 CalculateDirectionAndSpread()
    {
        // Tạo ray từ camera (từ trung tâm màn hình)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        // Nếu ray va chạm với một vật thể, lấy điểm va chạm làm mục tiêu
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            // Nếu không có va chạm, lấy điểm cách xa từ camera
            targetPoint = ray.GetPoint(100);
        }

        // Tính toán hướng bắn
        Vector3 direction = targetPoint - bulletSpawn.position;

        // Thêm độ lan tỏa (spread) vào hướng bắn (randomize x, y)
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        
        

        // Trả về hướng đã được thêm độ lan tỏa
        return direction + new Vector3(0, y, z);
    }

    // Coroutine để hủy viên đạn sau thời gian sống của nó
    private IEnumerator DeytroyBulletAftterTime(GameObject bullet, float bulletPrefabLifeTime)
    {
        // Chờ trong khoảng thời gian viên đạn sống
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet); // Hủy viên đạn
    }
}

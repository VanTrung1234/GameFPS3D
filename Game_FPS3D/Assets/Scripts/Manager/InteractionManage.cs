using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InteractionManage : MonoBehaviour
{
    public static InteractionManage Instance;

    public Weapon hoveredWeapon=null;
    public AmmoBox hoveredAmmoBox=null;
    public Throwalbe hoveredThrowable =null;



    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            GameObject objectHitByRaycast = hit.transform.gameObject;
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon==false)
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                hoveredWeapon= objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled=true;
                if (Input.GetKeyDown (KeyCode.F))
                {
                    SoundManage.instance.pickUp.Play();
                    WeaponManage.Instance.PickupWeapon(objectHitByRaycast.gameObject); 
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            
            }
            else
            {
                if (hoveredWeapon)
                {
                    
                    hoveredWeapon.GetComponent<Outline>().enabled=false;
                }
            }

            //Ammo
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {

                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SoundManage.instance.pickUp.Play();
                    WeaponManage.Instance.PickupAmmo(hoveredAmmoBox);
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    Destroy(objectHitByRaycast.gameObject);
                }

            }
            else
            {
                if (hoveredAmmoBox)
                {

                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }

            //Nade
            if (objectHitByRaycast.GetComponent<Throwalbe>())
            {

                hoveredThrowable = objectHitByRaycast.gameObject.GetComponent<Throwalbe>();
                hoveredThrowable.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SoundManage.instance.pickUp.Play();
                    WeaponManage.Instance.PickupThrowable(hoveredThrowable);
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                     
                }

            }
            else
            {
                if (hoveredThrowable)
                {

                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }
            }

        }

    }
}

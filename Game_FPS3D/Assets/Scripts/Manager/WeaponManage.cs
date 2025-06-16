using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class WeaponManage : MonoBehaviour
{
    public static WeaponManage Instance;

    public Weapon hoveredWeapon = null;

    public List<GameObject> weaponSlots;

    public GameObject activeWeaponSlot;

    public int totalPistoAmmo = 0;
    public int totalRifleAmmo = 0;
    public int totalShotGunAmmo = 0;
    public int tepm = 0;
    public int total = 0;

    public int nade = 0;
    public float throwForce = 10f;


    public GameObject nadeSpawn;
    public float forceMultiplier = 0f;
    public float forceMultiplierLimt = 2f;

    public GameObject nadePrefab; 
    public int lethalsCount = 0;
    public Throwalbe.ThrowableType equippedLethalsType;

    public GameObject SmokePrefab;
    public int SmokeCount = 0;
    public Throwalbe.ThrowableType equippedSmokeType;


    private void Start()
    {

        activeWeaponSlot = weaponSlots[0];
        equippedLethalsType = Throwalbe.ThrowableType.Node;
        equippedSmokeType = Throwalbe.ThrowableType.Node;
    }
    private void Update()
    {
        foreach (GameObject weapon in weaponSlots)
        {
            if(weapon == activeWeaponSlot)
            {
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchAtiveSlost(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchAtiveSlost(1);
        }
        if (Input.GetKey(KeyCode.G)|| Input.GetKey(KeyCode.T))
        {
            forceMultiplier += Time.deltaTime;
            if (forceMultiplier > forceMultiplierLimt)
            {
                forceMultiplier = forceMultiplierLimt;
            }
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (lethalsCount > 0)
            {
                ThrowLethal();
            }
            forceMultiplier = 0f;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            if (SmokeCount > 0)
            {
                ThrowSmoke();
            }
            forceMultiplier = 0f;
        }

    }

    private void ThrowSmoke()
    {
        GameObject smokePrefab = getThrowPrefab(equippedSmokeType);
        GameObject throwable = Instantiate(smokePrefab, nadeSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rd = throwable.GetComponent<Rigidbody>();

        rd.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);
        throwable.GetComponent<Throwalbe>().hasBeenThrown = true;
        SmokeCount -= 1;
        if (SmokeCount <= 0)
        {
            equippedSmokeType = Throwalbe.ThrowableType.Node;
        }
        HUDManager.instance.UpdateThrowable();
    }

    private void ThrowLethal()
    {
        GameObject lethaPrefab= getThrowPrefab(equippedLethalsType);
        GameObject throwable = Instantiate(lethaPrefab, nadeSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rd=throwable.GetComponent<Rigidbody>();

        rd.AddForce(Camera.main.transform.forward*(throwForce*forceMultiplier),ForceMode.Impulse);
        throwable.GetComponent<Throwalbe>().hasBeenThrown = true;
        lethalsCount -= 1;
        if (lethalsCount <= 0)
        {
            equippedLethalsType = Throwalbe.ThrowableType.Node;
        }
        HUDManager.instance.UpdateThrowable();
    }

    private GameObject getThrowPrefab(Throwalbe.ThrowableType throwableType)
    {
        switch(throwableType)
        {
            case Throwalbe.ThrowableType.Nade:
                return nadePrefab;
            case Throwalbe.ThrowableType.Smoke:
                return SmokePrefab;

        }
        return new();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void PickupWeapon(GameObject pickUpWeapon)
    {
        AddWeaponIntoActiveSlot(pickUpWeapon);
        
    }
    public void AddWeaponIntoActiveSlot(GameObject weaponPickUp)
    {
        DropCurentWeapon(weaponPickUp);
        weaponPickUp.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = weaponPickUp.GetComponent<Weapon>();
        weaponPickUp.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        weaponPickUp.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);

        weapon.isActiveWeapon = true;
        
        weapon.hasTriggeredRecoil = true;
        weapon.animator.enabled = true;
        


    }

    private void DropCurentWeapon(GameObject weaponPickUp)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop=activeWeaponSlot.transform.GetChild(0).gameObject;
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon=false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;

            weaponToDrop.transform.SetParent(weaponPickUp.transform.parent);
            weaponToDrop.transform.localPosition=weaponPickUp.transform.localPosition;
            weaponToDrop.transform.localRotation=weaponPickUp.transform.localRotation;
        }
    }
    public void SwitchAtiveSlost(int slotNumber)
    {
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapon= activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapon.isActiveWeapon=false;

        }
        activeWeaponSlot= weaponSlots[slotNumber];
        if(activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = true;
            newWeapon.animator.enabled = false;
        }
    }

    public void PickupAmmo(AmmoBox ammoBox)
    {
        switch(ammoBox.typeAmmo) 
        {
            case AmmoBox.TypeAmmo.RifleAmmo:
                totalRifleAmmo += ammoBox.AmmoAmout;
                break;

            case AmmoBox.TypeAmmo.PistoAmmo:                
                totalPistoAmmo += ammoBox.AmmoAmout;
                break;

            case AmmoBox.TypeAmmo.ShootGunAmmo:
                totalShotGunAmmo += ammoBox.AmmoAmout;
                break;
        
        }
    }

    internal void DecreaseTotalAmmo(int bulletsLeft, Weapon.WeaponModel weaponmodel, int magazineSize)
    {
        
        
        switch (weaponmodel)
        {
            
            case Weapon.WeaponModel.M4_8:
                tepm = magazineSize - bulletsLeft;
                total = bulletsLeft + totalRifleAmmo;
                if (totalRifleAmmo>magazineSize || total >= magazineSize)
                    totalRifleAmmo -= tepm;
                else
                    totalRifleAmmo -= totalRifleAmmo;
                break;
            case Weapon.WeaponModel.M1911:
                tepm = magazineSize - bulletsLeft;
                total = bulletsLeft + totalPistoAmmo;
                if (totalPistoAmmo > magazineSize || total >= magazineSize)
                {
                    
                    
                    totalPistoAmmo -= tepm;
                }
                else
                    totalPistoAmmo -= totalPistoAmmo;
                break;
            case Weapon.WeaponModel.Bennelli_M4:
                tepm = magazineSize - bulletsLeft;
                total = bulletsLeft + totalShotGunAmmo;
                if (totalShotGunAmmo > magazineSize || total >= magazineSize)
                    totalShotGunAmmo -= tepm;
                else
                    totalShotGunAmmo -= totalShotGunAmmo;
                break;
        }

    }
    public int checkAmmoLeftFor(Weapon.WeaponModel weaponmodel)
    {
        switch (weaponmodel)
        {
            case Weapon.WeaponModel.M4_8:
                return totalRifleAmmo;
            case Weapon.WeaponModel.M1911:
                return totalPistoAmmo;
            case Weapon.WeaponModel.Bennelli_M4:
                return totalShotGunAmmo;
            default: return 0;

        }
    }
    public void PickupThrowable(Throwalbe hoveredNade)
    {
        switch (hoveredNade.type)
        {
            case Throwalbe.ThrowableType.Nade:
                PickupAslethal(Throwalbe.ThrowableType.Nade);
                break;
            case Throwalbe.ThrowableType.Smoke:
                PickupAsSmoke(Throwalbe.ThrowableType.Smoke);
                break;
        }
    }

    private void PickupAsSmoke(Throwalbe.ThrowableType smoke)
    {

        if (equippedSmokeType == smoke || equippedSmokeType == Throwalbe.ThrowableType.Node)
        {
            equippedSmokeType = smoke;
            if (SmokeCount < 2)
            {
                SmokeCount += 1;
                Destroy(InteractionManage.Instance.hoveredThrowable.gameObject);
                HUDManager.instance.UpdateThrowable();
            }

            else
            {
                print("trung");
            }
        }
        else
        {

        }


    }

    public void PickupAslethal(Throwalbe.ThrowableType letha)
    {
        if(equippedLethalsType== letha|| equippedLethalsType== Throwalbe.ThrowableType.Node)
        {
            equippedLethalsType= letha;
            if (lethalsCount < 2)
            {
                lethalsCount += 1;
                Destroy(InteractionManage.Instance.hoveredThrowable.gameObject);
                HUDManager.instance.UpdateThrowable();
            }

            else
            {
                print("trung");
            }
        }
        else
        {

        }

        
    }
}

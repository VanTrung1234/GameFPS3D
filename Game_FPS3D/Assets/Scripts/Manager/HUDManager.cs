using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;


    public TextMeshProUGUI MagazineAmmoUI;
    public TextMeshProUGUI TotalAmmoUI;
    public Image AmmoTypeUI;

    public Image ActiveWeaponUI;
    public Image UnActiveWeaponUI;

    public Image LethalUI;
    public TextMeshProUGUI LethalAmountUI;

    public Image TacticalUI;
    public TextMeshProUGUI TacticalAmountUI;

    public Sprite emptySlot;
    public Sprite graySlot;


    public GameObject middleDot;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
        
    private void Update()
    {
            Weapon activeWeapon= WeaponManage.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
            Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();
            

        


        if (activeWeapon )
        {

            updateBullet(activeWeapon);

            Weapon.WeaponModel model =activeWeapon.weaponmodel;
            AmmoTypeUI.sprite = GetAmmoSprie(model);

            ActiveWeaponUI.sprite = GetWeaponSprie(model);

            if (unActiveWeapon)
            {

                UnActiveWeaponUI.sprite = GetWeaponSprie(unActiveWeapon.weaponmodel);
            }
            else
            {
                UnActiveWeaponUI.sprite = emptySlot;
            }

        }
        else
        {
            
            if(unActiveWeapon )
            {
                MagazineAmmoUI.text = "";
                TotalAmmoUI.text = "";
                AmmoTypeUI.sprite = emptySlot;
                ActiveWeaponUI.sprite = emptySlot;
                Debug.Log(unActiveWeapon.weaponmodel);
                UnActiveWeaponUI.sprite = GetWeaponSprie(unActiveWeapon.weaponmodel);
            }
            else
            {
                MagazineAmmoUI.text = "";
                TotalAmmoUI.text = "";
                AmmoTypeUI.sprite = emptySlot;
                ActiveWeaponUI.sprite = emptySlot;
                UnActiveWeaponUI.sprite = emptySlot;
            }
            if (WeaponManage.Instance.lethalsCount <= 0)
            {
                LethalUI.sprite = graySlot;
                
            }
            if (WeaponManage.Instance.SmokeCount <= 0)
            {
                TacticalUI.sprite = graySlot;

            }
        }
    }

    private void updateBullet(Weapon weapon)
    {
       
        
            MagazineAmmoUI.text = $"{weapon.bulletsLeft }";
            TotalAmmoUI.text =$"{WeaponManage.Instance.checkAmmoLeftFor(weapon.weaponmodel)}";
        
        
        
        
        
    }
    private Sprite GetWeaponSprie(Weapon.WeaponModel model)
    {
        switch(model)
        {
            case Weapon.WeaponModel.M1911:
                return Resources.Load<GameObject>("M1911_Weapon").GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M4_8:
                return Resources.Load<GameObject>("M4_8_Weapon").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.Bennelli_M4:
                return Resources.Load<GameObject>("Bennelli_M4_Weapon").GetComponent<SpriteRenderer>().sprite;

            default: return null;
        }
    }
    private Sprite GetAmmoSprie(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.M1911:
                return Resources.Load<GameObject>("M1911_Ammo").GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.M4_8:
                return Resources.Load<GameObject>("M4_8_Ammo").GetComponent<SpriteRenderer>().sprite;

            case Weapon.WeaponModel.Bennelli_M4:
                return Resources.Load<GameObject>("Bennelli_M4_Ammo").GetComponent<SpriteRenderer>().sprite;

            default: return null;
        }
    }
    private GameObject GetUnActiveWeaponSlot()
    {
        foreach(GameObject weaponSlot in WeaponManage.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManage.Instance.activeWeaponSlot) { 
                return weaponSlot;
            
            }
        }
        return null;
    }

    internal void UpdateThrowable()
    {
        LethalAmountUI.text = $"{WeaponManage.Instance.lethalsCount}";
        TacticalAmountUI.text = $"{WeaponManage.Instance.SmokeCount}";
        switch (WeaponManage.Instance.equippedLethalsType)
        {
            case Throwalbe.ThrowableType.Nade:
                
                LethalUI.sprite = Resources.Load<GameObject>("Nade").GetComponent<SpriteRenderer>().sprite;
                break;
        }


        
        switch (WeaponManage.Instance.equippedSmokeType)
        {
            case Throwalbe.ThrowableType.Smoke:

                TacticalUI.sprite = Resources.Load<GameObject>("Smoke").GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }
}

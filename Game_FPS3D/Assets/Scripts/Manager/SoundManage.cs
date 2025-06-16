using UnityEngine;
using static Weapon;

public class SoundManage : MonoBehaviour
{
    [Header("Weapon")]
    public static SoundManage instance;
    public AudioSource shootingSource;

    public AudioSource magazineSourcem1911;
    

    public AudioClip shootingClipM1991;
    public AudioClip shootingClipM4_8;
    public AudioClip shootingClipBennlli_M4;


    public AudioSource reloadSourcem1911;
    public AudioSource reloadSourceM4_8;
    public AudioSource reloadSourceBennlli_M4;

    [Header("Thwornd")]
    public AudioSource NadeChanle;
    public AudioClip Nade;

    public AudioSource smokeChanle;
    public AudioClip smoke;

    [Header("PickUp")]
    public AudioSource pickUp;

    [Header("Zombie")]
    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDead;

    public AudioSource zombieSource;
    public AudioSource zombieSource1;

    [Header("Player")]
    public AudioSource playerSource;
    public AudioClip playerHurt;
    public AudioClip playerDead;

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

    public void PlayShootingSound(WeaponModel weaponModel)
    {
        switch (weaponModel)
        {
            case WeaponModel.M1911:
                shootingSource.PlayOneShot(shootingClipM1991);
                break;
            case WeaponModel.M4_8:
                shootingSource.PlayOneShot(shootingClipM4_8);
                break;
            case WeaponModel.Bennelli_M4:
                shootingSource.PlayOneShot(shootingClipBennlli_M4);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weaponModel)
    {
        switch (weaponModel)
        {
            case WeaponModel.M1911:
                reloadSourcem1911.Play();
                break;
            case WeaponModel.M4_8:
                reloadSourceM4_8.Play();
                break;
            case WeaponModel.Bennelli_M4:
                reloadSourceBennlli_M4.Play();
                break;
        }
    }
}

using System;
using UnityEngine;

public class Throwalbe : MonoBehaviour
{
    public int nadeDamge=0;

    public float delay = 3f;
    public float damageRadis = 20f;
    public float explosionForce = 1200f;

    float countDown;
    bool hasExploded=false;
    public bool hasBeenThrown=false;

    public enum ThrowableType
    {
        Node,
        Nade,
        Smoke
    }


    public ThrowableType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenThrown)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();
        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        switch (type)
        {
                case ThrowableType.Nade:
                NadeEffect();
                break;
                case ThrowableType.Smoke:
                SmokeEffect();
                break;
        }
    }

    private void SmokeEffect()
    {
        GameObject smoleEffect = GobalReferences.instance.smokeExposionEffect;
        Instantiate(smoleEffect, transform.position, transform.rotation);

        SoundManage.instance.smokeChanle.PlayOneShot(SoundManage.instance.smoke);

        Collider[] collider = Physics.OverlapSphere(transform.position, damageRadis);
        foreach (Collider collider2 in collider)
        {
            Rigidbody rb = collider2.GetComponent<Rigidbody>();
            if (rb != null)
            {
               // rb.AddExplosionForce(explosionForce, transform.position, damageRadis);
            }
        }
    }

    private void NadeEffect()
    {
        GameObject exposionEffect = GobalReferences.instance.nadeExposionEffect;
        Instantiate(exposionEffect,transform.position,transform.rotation);

        SoundManage.instance.NadeChanle.PlayOneShot(SoundManage.instance.Nade);

        Collider[] collider = Physics.OverlapSphere(transform.position, damageRadis);
        foreach (Collider collider2 in collider)
        {
            Rigidbody rb=collider2.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce,transform.position,damageRadis);
            }
            if (collider2.GetComponent<Zombie>())
            {
                collider2.GetComponent<Zombie>().takeDamage(nadeDamge);
            }
        }
    }
}

using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Taget"))
        {
            print("hit " + collision.gameObject.name + " !");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit a Wall");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Beer"))
        {
            print("hit a Beer");
            collision.gameObject.GetComponent<BeerBottle>().Shatter();
            
        }
        if (collision.gameObject.CompareTag("Zombie"))
        {
            print("hit a Beer");
            if (collision.gameObject.GetComponent<Zombie>().isDead == false)
            {
                collision.gameObject.GetComponent<Zombie>().takeDamage(bulletDamage);
            }
            
            createBloodSprayEffect(collision);
            Destroy(gameObject);

        }
    }

    private void createBloodSprayEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject hole = Instantiate(
            GobalReferences.instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(collision.gameObject.transform);
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact=collision.contacts[0];
        GameObject hole = Instantiate(
            GobalReferences.instance.bulletEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(collision.gameObject.transform);


    }
        

}

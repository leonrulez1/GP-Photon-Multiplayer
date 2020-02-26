using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 30;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    public enum ammoType { one , two}
    public ammoType ammo;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        switch (ammo)
        {
            case ammoType.one:
             Boss boss = collision.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        Destroy(Instantiate(impactEffect, transform.position, transform.rotation), 2f);
        Destroy(gameObject);
                break;

            //case ammoType.two:
            //    //Health hp = collision.GetComponent<Health>();
            //    if (hp != null)
            //    {
            //        print("hit");
            //        //hp.TakeDamage();
            //    }
            //    Destroy(Instantiate(impactEffect, transform.position, transform.rotation), 2f);
            //    Destroy(gameObject);
            //    break;
        }
       
        
    }
}

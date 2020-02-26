using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 1000;
    public GameObject deathEffect;
    public GameObject projectile;
    public GameObject[] firePoints;
    public GameObject[] spikes;
    public GameObject[] meteorPoints;
    public GameObject[] spawnPoints;
    public GameObject Immunity;
    public List<string> possibleAttacks = new List<string>() { "Horizontal" };
    public List<string> possibleAttacks2 = new List<string>() { "Laser" };

    public enum BossType { one, two, three }
    public BossType bossType;


     void Start()
    {
        StartCoroutine(Attack());
        print("Attack");
    }

    public void TakeDamage(int damage)
    {
        if (bossType == BossType.one)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
            else if (health <= 200)
            {
                if (!possibleAttacks.Contains("Down"))
                    possibleAttacks.Add("Down");
                print("Down");
            }
            else if (health <= 500)
            {
                if (!possibleAttacks.Contains("Up"))
                    possibleAttacks.Add("Up");

                print("Up");
            }
        }
        else
        {
            
            
        }
       
       
        
    }
    
    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void Executable(string currentAttack)
    {
        //print(currentAttack);
        switch (currentAttack)
        {
            case "Horizontal":
               // Instantiate(projectile, firePoints[0].transform.position, firePoints[0].transform.rotation);
                StartCoroutine(HorizontalAttack());
                print("horizontal");
                break;

            case "Up":
                StartCoroutine(Up());
                print("Up");
                break;

            case "Down":
                StartCoroutine(Down());
               // Instantiate(projectile, firePoints.transform.position, firePoint1.transform.rotation);
                print("Down");
                break;
        }
    }

    void LaserBoss(string bossAttacks2)
    {
        switch (bossAttacks2)
        {
            case "Laser":
                StartCoroutine(Laser());
                break;

            case "Spawn":
                StartCoroutine(Spawn());
                break;

            case "Immune":
                Immune();
                break;
        }
    }

    IEnumerator Attack()
    {
        switch (bossType) {

            // AI for Boss 1
            case BossType.one:

                while (true)
                {
                    yield return new WaitForSeconds(2f);
                    Executable(possibleAttacks[Random.Range(0, possibleAttacks.Count)]);
                    yield return new WaitForSeconds(2f);
                }
                break;

            case BossType.two:
                while (true)
                {
                    yield return new WaitForSeconds(2f);
                    LaserBoss(possibleAttacks2[Random.Range(0, possibleAttacks2.Count)]);
                    yield return new WaitForSeconds(2f);
                    
                }
                break;
        }
    }
    IEnumerator HorizontalAttack()
    {
        for(int i = 0; i < firePoints.Length; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(projectile, firePoints[i].transform.position, firePoints[i].transform.rotation);
        }
    }
    IEnumerator Up()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            yield return new WaitForSeconds(0.4f);
            spikes[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            spikes[i].SetActive(false);


        }
    }
    IEnumerator Down()
    {
        for (int i = 0; i < meteorPoints.Length; i++)
        {
            yield return new WaitForSeconds(0.4f);
            Instantiate(projectile, meteorPoints[i].transform.position, meteorPoints[i].transform.rotation);
        }
    }

    IEnumerator Laser()
    {
        print("Laser");
        Laser laser = GetComponent<Laser>();
        
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            yield return new WaitForSeconds(0.4f);
            spawnPoints[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            spawnPoints[i].SetActive(false);


        }
    }

    void Immune()
    {
        Immunity.SetActive(true);

    }
}

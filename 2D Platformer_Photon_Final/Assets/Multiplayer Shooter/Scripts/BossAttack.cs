using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BossAttack : MonoBehaviourPun
{
    public GameObject projectile;
    public GameObject[] firePoints;
    public GameObject[] spikes;
    public GameObject[] meteorPoints;
    public bool attack = false;
    float timer = 3f;

    public List<string> possibleAttacks = new List<string>() { "Horizontal" };

    // Start is called before the first frame update
    void Start()
    {
        attack = true;
        this.GetComponent<PhotonView>().RPC("Attacking", RpcTarget.AllBuffered);
        print("Tested");
        
    }

    // Update is called once per frame
    
    void Update()
    {
        //if (attack == true)
        //{
        //    Attacking();
        //    attack = false;
        //}
        //else
        //{
        //    return;
        //}
        // StartCoroutine(Attacking());
        if (timer <= 0)
        {
            timer = 3f;
            attack = true;
            print("reset");
        }
        timer -= Time.deltaTime;
        //print(timer);
        
        if (attack && timer <= 2)
        {
            Attacking();
            attack = false;
            //StartCoroutine(Attacking());
        }

    
    }

    
    IEnumerator Attack()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            
            yield return new WaitForSeconds(2f);
            Instantiate(projectile, firePoints[i].transform.position, firePoints[i].transform.rotation);
            //yield return new WaitForSeconds(2f);
            
            
            
        }
    }

    IEnumerator Attack2()
    {
        for (int i = 0; i < spikes.Length; i++)
        {

            yield return new WaitForSeconds(2f);
            Instantiate(projectile, spikes[i].transform.position, spikes[i].transform.rotation);

        }
    }

    IEnumerator Attack3()
    {
        for (int i = 0; i < meteorPoints.Length; i++)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(projectile, meteorPoints[i].transform.position, meteorPoints[i].transform.rotation);
            
        }
    }


    [PunRPC]
    void Attacking()
    {
        
        StartCoroutine(Attack());
        StartCoroutine(Attack2());
        StartCoroutine(Attack3());
       // yield return new WaitForSeconds(1f);
       // attack = false;
       // yield return new WaitForSeconds(3f);
      //  attack = true;
        //attack = false;
    }
}

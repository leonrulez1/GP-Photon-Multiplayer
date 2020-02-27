using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun {

    public bool MovingDirection;
    public float MoveSpeed = 8;
    
    public float DestroyTime=  2f;
    public float bulletDamage = 0.1f;

    public string killerName;
    public GameObject localPlayerObj;

    void Start()
    {
        if(photonView.IsMine)
        killerName = localPlayerObj.GetComponent<CowBoy>().MyName;
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(DestroyTime);
        // Destroy bullet on the other clients
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    }

    void Update()
    {
        if (!MovingDirection)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
        else {
            print("isItMoving");
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
    }


    [PunRPC]
    public void ChangeDirection()
    {
        print("ChangedDirection");
        MovingDirection = true;
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        print("checking");
    
        if (!photonView.IsMine)
        {
            return;
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        // if (target != null && (!target.IsMine))
        if (target != null)
        {
            print("targeted");
            //If any player is hit, update the health
            if (target.tag == "Boss")
            {
                print("HitBoss");
                //RPC is called on each client
                target.RPC("BossHealthUpdate", RpcTarget.AllBuffered, bulletDamage);
                print("hurted");
                target.GetComponent<HurtEffect>().GotHit();
               

                //health = fillImage.fillAmount;

                if (target.GetComponent<BossHealth>().health <= 0)
                {
                    // Only show on player who got killed
                    //Player GotKilled = target.Owner;
                    //target.RPC("YouGotKilledBy", GotKilled, killerName);
                    print("Dead");
                    //target.RPC("YouKilled", localPlayerObj.GetComponent<PhotonView>().Owner, target.Owner.NickName);
                }

            }
            this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);

        }
    }
}

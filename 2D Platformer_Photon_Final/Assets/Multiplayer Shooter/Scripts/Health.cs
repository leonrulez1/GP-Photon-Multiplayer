﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Health : MonoBehaviourPun {

    public Image fillImage;
    public float health = 10;
    

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D collider;
    public GameObject playerCanvas;

    public CowBoy playerScript;
    public GameObject KillGotKilledText;

    public void CheckHealth()
    {
        if (photonView.IsMine && health <= 0)
        {
            GameManager.instance.EnableRespawn();   //Revive count down can only be seen on our screen
            playerScript.DisableInputs = true;
            this.GetComponent<PhotonView>().RPC("death", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void death()
    {
        rb.gravityScale = 0;
        collider.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
        

    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 5;
        collider.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
        fillImage.fillAmount = 1;
        health = 10;
       
    }

    public void EnableInputs()
    {
        playerScript.DisableInputs = false;
    }


    [PunRPC]
    public void HealthUpdate(float damage = 0.1f)
    {

        fillImage.fillAmount -= damage;
        health = fillImage.fillAmount;
        CheckHealth();
    }

    [PunRPC]
    void YouGotKilledBy(string name)
    {
        GameObject go = Instantiate(KillGotKilledText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.KillGotKilledFeedBox.transform,false);
        go.GetComponent<Text>().text = "You Got Killed by : " + name;
        go.GetComponent<Text>().color = Color.red;
        Destroy(go, 3);
    }

    [PunRPC]
    void YouKilled(string name)
    {
        GameObject go = Instantiate(KillGotKilledText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.KillGotKilledFeedBox.transform, false);
        go.GetComponent<Text>().text = "You Killed : " + name;
        go.GetComponent<Text>().color = Color.green;
        Destroy(go, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            print("diededed");
            health = 0;
            print("0");
        }
    }
}

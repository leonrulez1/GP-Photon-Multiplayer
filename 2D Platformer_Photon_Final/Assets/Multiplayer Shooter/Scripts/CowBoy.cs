using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class CowBoy : MonoBehaviourPun {

    public float MoveSpeed = 5;
    public GameObject playerCam;
    public SpriteRenderer sprite;
    public PhotonView photonview;
    public  Animator anim;
    private bool AllowMoving = true;
    public AudioSource audio;
    public AudioClip gunShot;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPointRight;
    public Transform BulletSpawnPointleft;

    public Text PlayerName;
    public bool IsGrounded = false;
    public bool DisableInputs = false;  //When player dies, disable inputs
    private Rigidbody2D rb;
    public float jumpForce;

    public string MyName;

    // Use this for initialization
    void Awake ()
    {
        // If is my view
        if (photonView.IsMine)
        {
            GameManager.instance.LocalPlayer = this.gameObject;
            playerCam.SetActive(true);  //Make sure one camera for my instance, no other player's camera
            playerCam.transform.SetParent(null, false); //Detach the camera from the Player prefab
            PlayerName.text = "You : "+PhotonNetwork.NickName;  //Our view
            PlayerName.color = Color.green;
            MyName = PhotonNetwork.NickName;
        }
        else
        {
            PlayerName.text = photonview.Owner.NickName;    //Other players
            PlayerName.color = Color.red;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && !DisableInputs)
        {
            checkInputs();
        }
    }

    private void checkInputs()
    {
        if (AllowMoving)
        {
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
            transform.position += movement * MoveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && anim.GetBool("IsMove") == false)
        {
            shot();
            audio.PlayOneShot(gunShot);
        }
        else if (Input.GetKeyUp(KeyCode.RightControl))
        {
            anim.SetBool("IsShot", false);
            AllowMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("IsMove", true);
        }
        if (Input.GetKeyDown(KeyCode.D) && anim.GetBool("IsShot") == false)
        {
            //FlipSprite_Right()
            playerCam.GetComponent<CameraFollow2D>().offset = new Vector3(1.3f, 1.53f, 0);
            //Other clients will listen to this RPC and execute it
            photonview.RPC("FlipSprite_Right", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("IsMove", false);
        }

        if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsShot") == false)
        {
            //FlipSprite_Left()
            playerCam.GetComponent<CameraFollow2D>().offset = new Vector3(-1.3f, 1.53f, 0);
            photonview.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("IsMove", false);
        }
    }

    public void shot()
    {
        if (anim.GetBool("IsMove") == true)
            return;

        if (sprite.flipX == false)
        {
            GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointRight.position.x, BulletSpawnPointRight.position.y), Quaternion.identity, 0);
            bullet.GetComponent<Bullet>().localPlayerObj = this.gameObject;
        }
        
        if (sprite.flipX == true)
        {
            GameObject bullet = PhotonNetwork.Instantiate(BulletPrefab.name, new Vector2(BulletSpawnPointleft.position.x, BulletSpawnPointleft.position.y), Quaternion.identity, 0);
            bullet.GetComponent<Bullet>().localPlayerObj = this.gameObject;

            bullet.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
        }
        
        anim.SetBool("IsShot", true);
        AllowMoving = false;

    }

    public void shotUp() {
        anim.SetBool("IsShot", false);
        AllowMoving = true;
    }

    // RPC - Remote Procedure Call
    [PunRPC]
    private void FlipSprite_Right()
    {
        sprite.flipX = false;
    }

    [PunRPC]
    private void FlipSprite_Left()
    {
        sprite.flipX = true;
    }

    //Check if player is on the ground
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            IsGrounded = true;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            print("isgrounded");
            IsGrounded = false;
           // rb.velocity = Vector2.zero;
        }
    }

   public void Jump()
    {
        if(IsGrounded)
        rb.AddForce(new Vector2(0f, jumpForce /** Time.deltaTime*/));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public CharacterControls controls;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    public PhotonView pv;
    Vector3 smoothMove;

    private GameObject sceneCamera;
    public GameObject playerCamera;

    void Start()
    {
        if(photonView.IsMine)
        {
            playerCamera = GameObject.Find("Main Camera");
            sceneCamera.SetActive(false);
            playerCamera.SetActive(true); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


            if (Input.GetButtonDown("Jump"))
            {

                jump = true;
                animator.SetBool("Jumping", true);
            }

        }
        else
        {
            smoothMovement();
        }

    }

    private void FixedUpdate()
    {
       
        controls.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false; 

    }

   public void OnLanding ()
    {
        animator.SetBool("Jumping", false);
    }

    void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 7);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3) stream.ReceiveNext();
        }
    }

  
}

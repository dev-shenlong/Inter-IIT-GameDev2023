using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    public float RunSpeed = 40f;
    float dirX;
    float dirY;
    bool jump  = false;
    bool crouch = false;
    private enum MovementState { idle,running,jumping,falling}
    private Animator aanim;
    private Rigidbody2D rb;


    private bool canDash = true;
    private bool isDashing;
    private float Dashingpower =24f;
    private float Dashingtime = 0.2f;
    private float DashingCooldown = 1f;

    

    // Update is called once per frame
    [SerializeField] private bool isCharge = false;
    [SerializeField] private float maxJumpCharge = 500f;
    [SerializeField] private float JumpXSpeed = 60f;

    //for dashing
    [SerializeField] private TrailRenderer tr;


     void Start()
    {
        aanim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()

    {
        //dirX = Input.GetAxisRaw("Horizontal");
        // Debug.Log(controller.charge_Jumpforce);

        if (isDashing)
        { return; }

       if(!isCharge)
        {
            dirX = Input.GetAxisRaw("Horizontal") * RunSpeed;
        }
        dirY = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && controller.charge_Jumpforce<maxJumpCharge)
        {
            jump = false;
            isCharge = true;
        }
        if(isCharge)
        {
            controller.charge_Jumpforce += 9;
        }
        if(Input.GetButtonUp("Jump") || controller.charge_Jumpforce >=  maxJumpCharge )
        {
            jump = true;
            isCharge = false;
            dirX = Input.GetAxisRaw("Horizontal") * JumpXSpeed;

        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            StartCoroutine(Dash());
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }

        UpdateAnimation();


    }




    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalgravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * Dashingpower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(Dashingtime);
        tr.emitting = false;
        rb.gravityScale = originalgravity;
        isDashing = false;
        yield return new WaitForSeconds(DashingCooldown);
        canDash = true;



    }






    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        //move our character
        controller.Move(dirX*Time.fixedDeltaTime, crouch, jump);
        jump = false;  
    }





    private void UpdateAnimation()
    {
        MovementState state;
         
        if (dirX > 0f)
        {
            state = MovementState.running;
            
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
           
        }
        else
        {
            state = MovementState.idle;
        }



        if (rb.velocity.y>1.14f)
        {
            state = MovementState.jumping;
            Debug.Log(rb.velocity.y);
        }
        else if (rb.velocity.y < -1.14f)
        {
            state = MovementState.falling;

        }
        aanim.SetInteger("state", (int)state); 

        
    }

}

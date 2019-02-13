using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //*************************************/  Components on the player gameobject
    private Rigidbody2D rb;
    //*************************************/

    //*************************************/ variables for moving player
    [SerializeField] private float speed;
    private float moveInput;
    //************************************/

    //************************************/  variables for jump settings
    [SerializeField] private float checkRadius;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private float jumpTimeCounter;
    private bool isJumping;
    //************************************/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);

        if(isGrounded == true && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }


    void FixedUpdate()
    {
        move(moveInput);
    }

    private void move(float input)                       //The function that moves the player with rigidbody
    {
        rb.velocity = new Vector2(moveInput * speed,rb.velocity.y);
        
        if(input > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(input < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }
}

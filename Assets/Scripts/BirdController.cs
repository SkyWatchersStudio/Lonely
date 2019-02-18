using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    //******************************/ components of the bird
    private Rigidbody2D rb;
    /*******************************/

    //******************************/ settings for shapeshift progress
    [SerializeField] private float shapeShiftTime; 
    private float shapeShiftTimer;
    /******************************/

    //*****************************/ settings for moving The bird
    [SerializeField] private float speed;
    [SerializeField] private float flySpeed;
    private float moveInput;
    private Vector2 up;
    private Vector2 down;
    private Vector2 neutral;
    /******************************/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shapeShiftTimer = shapeShiftTime;
        
        up = new Vector2(rb.velocity.x,1.0f);
        down = new Vector2(rb.velocity.x,-1.0f);
        neutral = new Vector2(rb.velocity.x,0.0f);
    }

    void Update()
    {
        shapeShiftTimer -= Time.deltaTime;

        moveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetButton("Jump"))
        {
            rb.velocity = up;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            rb.velocity = neutral;
        }

        if(Input.GetButton("Down"))
        {
            rb.velocity = down;
        }
        else if(Input.GetButtonUp("Down"))
        {
            rb.velocity = neutral;
        }

        if(Input.GetButtonDown("ShapeShift"))
        {
            shapeShift();
        }

        checkTime();
    }

    void FixedUpdate()
    {
        move();
    }

    //function for moving and rotating the bird
    private void move()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y * flySpeed);

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    private void shapeShift()
    {
        
    }

    private void checkTime()
    {
        if(shapeShiftTimer <= 0)
        {
            shapeShift();
        }
    }
}

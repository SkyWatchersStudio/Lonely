using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    //******************************/ components of the bird
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    /*******************************/

    //******************************/ settings for shapeshift progress
    [SerializeField] private float shapeShiftTime; 
    private float shapeShiftTimer;
    /******************************/

    //*****************************/ settings for moving The bird
    [SerializeField] private float speed;
    [SerializeField] private float flySpeed;
    private float horizontalMove;
    private float verticalMove;
    /*****************************/
    
    //******************************/ variables for shapeshifting
    public Sprite player;
    private SpriteRenderer sp;
    private PlayerController pc;
    public Vector3 playerScale;
    /*******************************/

    void OnEnable()
    {
        shapeShiftTimer = shapeShiftTime;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CapsuleCollider2D>();

        rb.velocity = new Vector2(0.0f,0.0f);
    }

    void Update()
    {
        shapeShiftTimer -= Time.deltaTime;

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

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
        rb.velocity = new Vector2(horizontalMove * speed, verticalMove * flySpeed);

        if(horizontalMove > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(horizontalMove < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }

    private void shapeShift()
    {   
        cc.size = new Vector2(0.42f,0.94f);
        rb.gravityScale = 3;
        transform.localScale = playerScale;
        sp.sprite = player;
        pc.enabled = true;
        this.enabled = false;
    }

    private void checkTime()
    {
        if(shapeShiftTimer <= 0)
        {
            shapeShift();
        }
    }
}

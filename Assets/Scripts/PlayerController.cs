using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;
    [SerializeField] private float speed;

    //************************************/ attack values
    public Transform attackPos;
    [SerializeField] private float attackRadius;
    public LayerMask whatIsEnemy;
    /*************************************/
    //************************************/ getting hit values
    [SerializeField] private float freezeTime;
    [SerializeField] private float intense;
    private float freezeTimeCounter;
    private bool recovery = false;
    /*************************************/
    //***********************************/ jump values
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsground;
    [SerializeField] private float checkRadius;
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    private bool isGrounded;
    private bool isJumping;
    /************************************/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ( recovery == false )
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            isGrounded = Physics2D.OverlapCircle( groundCheck.position, checkRadius, whatIsground );

            if ( Input.GetButtonDown("Attack") )
            {
                attack();
            }

            if ( isGrounded && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }

            if ( Input.GetButton("Jump") )
            {
                if ( jumpTimeCounter > 0 && isJumping )
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if ( Input.GetButtonUp("Jump") )
                isJumping = false;
        }
        else if ( recovery == true )
        {
            freezeTimeCounter -= Time.deltaTime;

            if ( freezeTimeCounter <= 0.0f )
                recovery = false;
        }

        Debug.Log("intense " + intense);

    }

    void FixedUpdate()
    {
        if ( recovery == false )
            move();  
    }

    private void move()
    {
        rb.velocity = new Vector2( moveInput * speed, rb.velocity.y );

        if ( moveInput > 0.0f && transform.localScale.x == -1.0f)
        {
            Vector2 sc = transform.localScale;
            sc.x *= -1.0f;
            transform.localScale = sc;
        }
        else if ( moveInput < 0.0f && transform.localScale.x == 1.0f)
        {
            Vector2 sc = transform.localScale;
            sc.x *= -1.0f;
            transform.localScale = sc;            
        }
    }

    public void hurt( float dir )
    {
        float inte = Mathf.Abs(intense);
        inte *= dir;
        freezeTimeCounter = freezeTime;
        recovery = true;
        rb.velocity = new Vector2( inte, Mathf.Abs(inte) / 2 );
    }

    private void attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle( attackPos.position, attackRadius, whatIsEnemy);

        if ( enemy )
        {
            if ( enemy.transform.position.x > transform.position.x )
            {
                enemy.SendMessage( "hurt", 1.0f);
            }
            else if ( enemy.transform.position.x < transform.position.x )
            {
                enemy.SendMessage( "hurt", -1.0f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere( groundCheck.position, checkRadius );
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( attackPos.position, attackRadius );
    }
}

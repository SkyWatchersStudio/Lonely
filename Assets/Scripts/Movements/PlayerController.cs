using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TrainMovement TrainScript;

    [HideInInspector]
    public bool m_trigger = false;
    
    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    [SerializeField] private float speed;
    private bool facingRight = true;

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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (recovery == false)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(moveInput));

            isGrounded = Physics2D.OverlapCircle( groundCheck.position, checkRadius, whatIsground );

            if ( Input.GetButtonDown("Attack") )
            {
                Attack();
            }

            if ( isGrounded && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                anim.SetTrigger("Jump");
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
    }

    void FixedUpdate()
    {
        if ( recovery == false )
            Move();  
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0.0f && !facingRight)
            Flip();
        else if ( moveInput < 0.0f && facingRight)
            Flip();
    }

    public void Hurt(float dir)
    {
        float inte = Mathf.Abs(intense);
        inte *= dir;
        freezeTimeCounter = freezeTime;
        recovery = true;
        rb.velocity = new Vector2( inte, Mathf.Abs(inte) / 2 );
    }

    private void Attack()
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Train"))
    //    {
    //        speed = speed + TrainScript.m_Speed;
    //    }
    //}
}

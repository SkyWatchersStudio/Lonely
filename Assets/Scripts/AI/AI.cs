using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
    public StateMachine<AI> stateMachine {get;set;}

    //****************************************************/    components and valuse on the enemy object
    [HideInInspector] public Rigidbody2D rb;
    public Transform laserPos;
    public float speed;
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    /***************************************************/
    //**************************************************/      values for statemachine process
    [HideInInspector] public RaycastHit2D ray;
    public float distance = 5.0f;
    [HideInInspector] public Vector2 rayDirection;
    [HideInInspector] public Collider2D detectCol;
    [HideInInspector] public Transform player;
    public float detectRadius;
    /***************************************************/
    //**************************************************/      values for fight state
    [HideInInspector] public Collider2D colForAtt;
    public float attRadius;
    /**************************************************/ 
    //*************************************************/      getting hit values
    [SerializeField] private float freezeTime;
    [SerializeField] private float intense;
    private float freezeTimeCounter;
    private bool recovery;
    /**************************************************/
    //*************************************************/      jump values
    public float jumpForce;
    /**************************************************/

    private void Start()
    {   
        rayDirection = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<AI>(this);
        stateMachine.changeState(Patrol.Instance);
    }

    private void Update()
    {   
        colliderCheck();

        if ( recovery == false )
        {
            stateMachine.Update();
        }
        else if ( recovery )
        {
            freezeTimeCounter -= Time.deltaTime;

            if ( freezeTimeCounter <= 0.0f )
            {
                recovery = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if ( recovery == false)
            stateMachine.FixedUpdate();
    }

    private void colliderCheck()
    {
        ray = Physics2D.Raycast( laserPos.position, rayDirection, distance, wallLayer );

        detectCol = Physics2D.OverlapCircle( transform.position, detectRadius, playerLayer);

        if ( detectCol == true && stateMachine.currentState != Fight.Instance )
        {
            player = detectCol.transform;
            stateMachine.changeState(Fight.Instance);
        }
        else if ( detectCol == false && stateMachine.currentState == Fight.Instance )
        {
            stateMachine.changeState(Patrol.Instance);
            player = null;
        }
    }

    public void hurt( float dir )
    {
        intense *= dir;
        freezeTimeCounter = freezeTime;
        recovery = true;
        rb.velocity = new Vector2( intense, Mathf.Abs(intense) / 2 );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 to = new Vector2(laserPos.position.x + distance, laserPos.position.y);
        Gizmos.DrawLine( laserPos.position, to);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,detectRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(laserPos.position,attRadius);
    }
}

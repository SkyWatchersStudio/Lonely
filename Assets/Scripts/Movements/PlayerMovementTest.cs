using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float m_MoveSpeed = 10f;
    public float m_JumpForce = 700;
    public float m_Lerping = .25f;
    public float m_GroundRadious = .2f;
    public GameObject m_PauseObj;
    public Transform m_Ground;
    public LayerMask m_WhatIsGround;

    private Rigidbody2D m_rigidbody = null;
    private float m_moveInput = 0f;
    private Vector2 m_moveVel = Vector2.zero;
    private bool m_isPause = false;
    private bool m_isGround = true;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //check the ground...
        m_isGround = Physics2D.OverlapCircle((Vector2)m_Ground.position, m_GroundRadious, m_WhatIsGround);
        
        m_moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && m_isGround)
            m_rigidbody.AddForce(new Vector2(0, m_JumpForce));
        GetPauseInput();
    }
    private void FixedUpdate()
    {
        Moving();
    }
    void GetPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_isPause)
        {
            m_isPause = true;
            Time.timeScale = 0;
            m_PauseObj.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && m_isPause)
        {
            m_isPause = false;
            m_PauseObj.SetActive(false);
            Time.timeScale = 1;
        }
    }
    void Moving()
    {
        m_moveVel = Vector2.Lerp(m_rigidbody.velocity, m_moveInput * Vector2.right * m_MoveSpeed, m_Lerping);
        m_rigidbody.velocity = m_moveVel;
    }
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_Ground.position, m_GroundRadious);
    }
}
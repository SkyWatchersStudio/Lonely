using UnityEngine;

public class PMTest : MonoBehaviour
{
    public float m_JumpForce = 200;
    public float m_Speed = 10;
    private float m_inputAxis;
    private Rigidbody2D m_rigid;
    private Animator m_animator;
    private bool m_jump = false;
    private bool m_faceRight = true;

    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }
    private void Update() 
    {
        m_inputAxis = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && !m_jump)
            m_jump = true;

        if (m_inputAxis > 0 && !m_faceRight)
            Flip();
        else if (m_inputAxis < 0 && m_faceRight)
            Flip();
    }

    private void FixedUpdate() 
    {
        m_animator.SetFloat("Speed", Mathf.Abs(m_rigid.velocity.x)); //play animation

        m_rigid.velocity = new Vector2(m_inputAxis * m_Speed, m_rigid.velocity.y);

        //jump:
        if (m_jump)
        {
            m_rigid.AddForce(new Vector2(0, m_JumpForce));
            m_animator.SetTrigger("Jump");
            m_jump = false;
        }
    }
    private void Flip()
    {
        m_faceRight = !m_faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
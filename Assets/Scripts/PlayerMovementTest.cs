using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float m_MoveSpeed = 10f;

    private Rigidbody2D m_rigidbody = null;
    private float m_moveInput = 0f;
    private Vector2 m_moveDirect = Vector2.zero;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m_moveInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        m_moveDirect = Vector2.Lerp(m_moveDirect, m_moveInput * Vector2.right * m_MoveSpeed, .25f);
        if (Mathf.Abs(m_rigidbody.velocity.x) > m_MoveSpeed)
        {
            m_rigidbody.velocity = new Vector2
                            (Mathf.Sign(m_rigidbody.velocity.x) * m_MoveSpeed, m_rigidbody.velocity.y);
            return;
        }
        m_rigidbody.AddForce(m_moveDirect);
    }
}
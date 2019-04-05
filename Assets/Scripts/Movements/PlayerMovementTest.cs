using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float m_MoveSpeed = 10f;
    public float m_Lerping = .25f;
    public GameObject m_PauseObj;

    private Rigidbody2D m_rigidbody = null;
    private float m_moveInput = 0f;
    private Vector2 m_moveVel = Vector2.zero;
    private bool m_isPause = false;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        Moving();
    }


    void GetInput()
    {
        m_moveInput = Input.GetAxis("Horizontal");
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
}
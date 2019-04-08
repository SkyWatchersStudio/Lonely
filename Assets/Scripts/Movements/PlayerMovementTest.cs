using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float m_MoveSpeed = 10f;
    public float m_Lerping = .25f;
    public GameObject m_PauseObj;

    private Rigidbody2D m_rigidbody = null;
    private float m_moveInput = 0f;
    private float m_xVelocity = 0;
    private bool m_isPause = false;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m_moveInput = Input.GetAxis("Horizontal");
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
        m_xVelocity = Mathf.Lerp(m_rigidbody.velocity.x, m_moveInput * m_MoveSpeed, m_Lerping);
        m_rigidbody.velocity = new Vector2(m_xVelocity, m_rigidbody.velocity.y);
    }
}
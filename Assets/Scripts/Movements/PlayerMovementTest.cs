using UnityEngine;
using System.Collections;

public class PlayerMovementTest : MonoBehaviour
{
    public bool m_trigger = false;
    public float m_MaxSpeed = 7.57f;
    public float m_MoveSpeed = 4;
    public float m_MoveSpeedLerping = .25f;
    public float m_Lerping = 2.83f;
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
        //speed up player after train follow it
        if (m_trigger)
            m_MoveSpeed = Mathf.Lerp(m_MoveSpeed, m_MaxSpeed, Time.fixedDeltaTime * m_MoveSpeedLerping);
        Moving();
    }
    //pause the game...
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
        //speed up the player movement slowly
        m_xVelocity = Mathf.Lerp(m_rigidbody.velocity.x, m_moveInput * m_MoveSpeed, m_Lerping);
        m_rigidbody.velocity = new Vector2(m_xVelocity, m_rigidbody.velocity.y);
    }
}
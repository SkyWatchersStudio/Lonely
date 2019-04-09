using UnityEngine;
using System.Collections;

public class PlayerMovementTest : MonoBehaviour
{
    public float m_MaxSpeed = 7.57f;
    public float m_MoveSpeed = 4;
    public float m_MoveSpeedLerping = .25f;
    public float m_Lerping = 2.83f;
    public float m_WaitForTrain = 1.5f;
    public GameObject m_PauseObj;
    public TriggerLever m_Lever;
    public GameObject m_LocoLight, m_LocoSteam;

    private Rigidbody2D m_rigidbody = null;
    private float m_moveInput = 0f;
    private float m_xVelocity = 0;
    private bool m_isPause = false;
    private bool m_trigger = true;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (m_Lever.IsTriggered && m_trigger)
        {
            m_LocoLight.SetActive(true);
            m_LocoSteam.SetActive(true);
            StartCoroutine(CinematicForTrain());
            m_trigger = false;
        }
        if (m_Lever.IsTriggered)
            m_MoveSpeed = Mathf.Lerp(m_MoveSpeed, m_MaxSpeed, Time.deltaTime * m_MoveSpeedLerping);
        m_moveInput = Input.GetAxis("Horizontal");
        GetPauseInput();
    }
    private IEnumerator CinematicForTrain()
    {
        this.enabled = false;
        yield return new WaitForSeconds(m_WaitForTrain);
        this.enabled = true;
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
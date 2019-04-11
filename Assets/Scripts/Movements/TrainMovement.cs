using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float m_Speed = 1;
    public float m_MaxSpeed = 100;
    //damping speed
    public float m_SmoothTime = 22.48f;
    //see if lever moved
    public TriggerLever m_LeverScript;

    //variable used for smooth damp
    private float m_cVelocity;
    private Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        #if !UNITY_EDITOR
        if (m_LeverScript.IsTriggered)
        {
            move();
        }
        #endif
    }
    private void move()
    {
        //Make a new 'velocity' for train
        m_rb.velocity = new Vector2(m_Speed, m_rb.velocity.y);
        //moving speed toward max speed in the specified time...
        m_Speed = Mathf.SmoothDamp(m_Speed, m_MaxSpeed, ref m_cVelocity, m_SmoothTime);
    }
    //break objects in front of train
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            other.gameObject.SetActive(false);
        }
        #if !UNITY_EDITOR
        else if (other.gameObject.tag == "Player" && m_LeverScript.IsTriggered)
        {
            other.gameObject.SetActive(false);
            StartCoroutine(WaitPlayerDeath());
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
    }
}

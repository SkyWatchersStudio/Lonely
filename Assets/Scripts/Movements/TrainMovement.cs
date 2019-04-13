using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float m_Speed = 1;
    public float m_MaxSpeed = 100;
    //damping speed
    public float m_SmoothTime = 22.48f;

    //variable used for smooth damp
    private float m_cVelocity;
    private Rigidbody2D m_rb;
    private bool m_trigger = false;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (m_trigger)
        {
            move();
        }
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
            other.gameObject.SetActive(false);
        else if (other.gameObject.tag == "Player" && m_trigger)
        {
            other.gameObject.SetActive(false);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    public void ChangeWithTrigger()
    {
        m_rb.bodyType = RigidbodyType2D.Kinematic;
        m_rb.useFullKinematicContacts = true;
        m_rb.sleepMode = RigidbodySleepMode2D.StartAsleep;
        m_rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        m_trigger = true;
    }
}

using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float m_Speed = 10f;
    public float m_MaxSpeed = 20;
    public float m_SmoothTime = 1.78f;

    //variable used for smooth damp
    private float m_cVelocity;
    private bool m_trigger = false;
    private Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //trigger the train that will start moving...
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
        Vector2 moveSpeed;
        //Make a new 'velocity' for train
        moveSpeed = new Vector2(m_Speed, m_rb.velocity.y);
        
        m_rb.velocity = moveSpeed;
        //moving speed toward max speed in the specified time...
        m_Speed = Mathf.SmoothDamp(m_Speed, m_MaxSpeed, ref m_cVelocity, m_SmoothTime);
    }
}

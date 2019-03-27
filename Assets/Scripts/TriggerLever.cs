using UnityEngine;

public class TriggerLever : MonoBehaviour
{
    public float m_Force = 20;
    
    private Rigidbody2D m_rigidbody;
    private bool m_trigger = false;
    private void Awake() 
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
            m_trigger = true;
    }
    private void FixedUpdate() 
    {
        if (m_trigger)
        {
            m_rigidbody.AddForce(new Vector2(m_Force, 0), ForceMode2D.Impulse);
            m_trigger = false;
        }
    }
}
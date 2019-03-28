using UnityEngine;

public class TriggerLever : MonoBehaviour
{
    public float m_Force = 30;
    public Transform m_AnchorPoint;
    public BoxCollider2D m_Collider;

    private bool m_trigger = false;
    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigidbody;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, m_Collider.size);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        OnTriggerEnter2D(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                SetValues();
                //Trigger the lever
                m_rigidbody.AddForce(new Vector2(m_Force, 0), ForceMode2D.Impulse);
                m_trigger = true;
            }
            if (m_trigger)
                if (m_rigidbody.velocity == Vector2.zero)
                    this.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        OnTriggerStay2D(other);
    }
    private void SetValues()
    {
        //Refrences:
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_joint = gameObject.AddComponent<HingeJoint2D>();
        JointAngleLimits2D angle = new JointAngleLimits2D();

        //limit angle
        angle.max = 45;
        //Set joint at desire
        m_joint.limits = angle;
        m_joint.anchor = transform.InverseTransformPoint(m_AnchorPoint.position);
    }
    //Destroy Components:
    private void OnDisable()
    {
        Destroy(m_joint);
        Destroy(m_rigidbody);
    }
}
using UnityEngine;

public class TriggerLever : MonoBehaviour
{
    public float m_Force = 20;
    public Vector2 m_AnchorPoint;

    private bool m_trigger = false;
    private bool m_secondTrigger = false;
    private JointAngleLimits2D m_angle;
    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        //limit angle
        m_angle.max = 45;

        //Refrences:
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_joint = gameObject.AddComponent<HingeJoint2D>();
        //Set joint at desire
        m_joint.limits = m_angle;
        m_joint.anchor = m_AnchorPoint;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Trigger the lever
            m_rigidbody.AddForce(new Vector2(m_Force, 0), ForceMode2D.Impulse);
            m_trigger = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
            m_secondTrigger = true;
    }
    private void FixedUpdate()
    {
        if (m_trigger)
        {
            if (m_rigidbody.velocity != Vector2.zero)
                return;
            Destroy(m_joint);
            Destroy(m_rigidbody);
            this.enabled = false;
        }
    }
}
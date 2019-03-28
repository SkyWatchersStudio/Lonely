using UnityEngine;

public class TriggerLever : MonoBehaviour
{
    public float m_Force = 20;
    public Transform m_AnchorPoint;

    private bool m_trigger = false;
    private JointAngleLimits2D m_angle;
    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigidbody;

    private void Update()
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
    private void SetValues()
    {
        //Refrences:
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_joint = gameObject.AddComponent<HingeJoint2D>();

        //limit angle
        m_angle.max = 45;
        //Set joint at desire
        m_joint.limits = m_angle;
        m_joint.anchor = transform.InverseTransformPoint(m_AnchorPoint.position);
    }
    private void OnDisable()
    {
        Destroy(m_joint);
        Destroy(m_rigidbody);
    }
}
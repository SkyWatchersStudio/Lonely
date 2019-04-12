using UnityEngine;

public class TriggerLever : EventTriggers
{
    public float m_Force = 30;
    public Transform m_AnchorPoint;
    public Transform m_VCam;
    public TrainMovement m_Train;
    public PlayerMovementTest m_PlayerScript;
    public GameObject m_LocoSteam, m_LocoLight;

    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigidbody;
    private bool m_trigger = false;
    private GameObject m_beforeTrainCam, m_afterTrainCam;

    private void Start()
    {
        m_beforeTrainCam = m_VCam.GetChild(0).gameObject;
        m_afterTrainCam = m_VCam.GetChild(1).gameObject;

        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    public override void OnTriggerEnter2D(Collider2D other) {}
    public override void OnTriggerStay2D(Collider2D other)
    {
        EnteredTrigger(other);
    }
    public override void OnTriggerExit2D() {}
    private void EnteredTrigger(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
        {
            SetValues();
            //Trigger the lever
            m_rigidbody.AddForce(new Vector2(m_Force, 0), ForceMode2D.Impulse);

            //set the second camera as desire camera
            m_beforeTrainCam.SetActive(false);
            m_afterTrainCam.SetActive(true);

            m_trigger = true;
        }
    }
    private void LateUpdate()
    {
        //if we triggered the lever and force applied and lever is standing
        if (m_trigger && m_rigidbody.velocity == Vector2.zero)
        {
            //call the event trigger...
            m_Train.ChangeWithTrigger();
            m_PlayerScript.m_trigger = true;

            //turn on train
            m_LocoLight.SetActive(true);
            m_LocoSteam.SetActive(true);
            
            m_rigidbody.simulated = false;
            Destroy(this);
        }
    }
    private void SetValues()
    {
        m_joint = gameObject.AddComponent<HingeJoint2D>();

        //rigidbody desire values:
        m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        m_rigidbody.gravityScale = 0;
        m_rigidbody.mass = 2.16f;
        m_rigidbody.angularDrag = .31f;
        m_rigidbody.sleepMode = RigidbodySleepMode2D.StartAsleep;
        //limit angle
        JointAngleLimits2D angle = m_joint.limits;
        angle.max = 45;
        //Set joint at desire
        m_joint.limits = angle;
        m_joint.anchor = transform.InverseTransformPoint(m_AnchorPoint.position);
    }
}
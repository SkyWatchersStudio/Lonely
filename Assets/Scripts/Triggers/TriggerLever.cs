using UnityEngine;

public class TriggerLever : MonoBehaviour
{
    public float m_Force = 30;
    public Transform m_AnchorPoint;
    //public Transform m_VCam;
    public TrainMovement m_Train;
    public GameObject m_LocoSteam, m_LocoLight;

    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigidbody;
    bool m_trigger = false;
    private GameObject m_beforeTrainCam, m_afterTrainCam;

    private void Start()
    {
        //m_beforeTrainCam = m_VCam.GetChild(0).gameObject;
        //m_afterTrainCam = m_VCam.GetChild(1).gameObject;

        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            //setup lever for pushing it...
            SetValues();
            //set the trigger to true
            m_trigger = true;
            //Add force to the lever
            m_rigidbody.AddForce(new Vector2(m_Force, 0), ForceMode2D.Impulse);
        }
        else if (m_trigger)
        {
            PushLever();
        }
    }
    private void PushLever()
    {
        if (m_rigidbody.velocity != Vector2.zero)
        {
            return;
        }
        //set the second camera as desire camera
        //m_beforeTrainCam.SetActive(false);
        //m_afterTrainCam.SetActive(true);

        //call the event trigger...
        m_Train.ChangeWithTrigger();
        // m_PlayerScript.m_trigger = true; //speed the player up to run away from train

        //turn on train
        m_LocoLight.SetActive(true);
        m_LocoSteam.SetActive(true);

        //disable simulated will disable physics of this object and also remove it from memroy
        m_rigidbody.simulated = false;
        Destroy(this);
    }
    private void SetValues()
    {
        //add references and setup components
        m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        m_joint = gameObject.AddComponent<HingeJoint2D>();
        JointAngleLimits2D angle = m_joint.limits;

        //rigidbody desire values:
        m_rigidbody.gravityScale = 0;
        m_rigidbody.angularDrag = .31f;
        m_rigidbody.sleepMode = RigidbodySleepMode2D.StartAsleep;
        //limit angle
        angle.max = 45;
        //Set joint at desire
        m_joint.limits = angle;
        //convert world space into local space to use with anchor
        m_joint.anchor = transform.InverseTransformPoint(m_AnchorPoint.position);
    }
}
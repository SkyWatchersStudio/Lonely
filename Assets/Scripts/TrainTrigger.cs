using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public float m_Speed = 10f;
    public float m_MaxSpeed = 20;
    public float m_SmoothTime = 1.78f;
    public AudioSource m_TrainAudio;
    public float m_PitchRange = .2f;
    public float m_NearEndClip = .5f;
    [Range(0.0f, 1.0f)] public float m_MinVolume = .2f;

    private float m_cVelocity;
    private bool m_trigger = false;
    private Rigidbody2D m_rb;
    private Vector2 m_moveSpeed;
    private Vector2 m_currentVel;
    private float m_orginalPitch;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_orginalPitch = m_TrainAudio.pitch;
    }
    void Update()
    {
        //trigger the train that will start moving...
        if (Input.GetKeyDown(KeyCode.S))
            m_trigger = true;
    }
    private void FixedUpdate()
    {
        if (m_trigger)
        {
            move();
            PlayAudio();
        }
    }
    private void PlayAudio()
    {
        if (! m_TrainAudio.isPlaying)
        {
            //every time want to play song change the pitch will make new sound
            m_TrainAudio.pitch = Random.Range(m_orginalPitch - m_PitchRange, m_orginalPitch + m_PitchRange);
            m_TrainAudio.Play();
        }
        
        //speed up = volume up
        m_TrainAudio.volume = Mathf.Max(m_MinVolume, m_Speed / m_MaxSpeed);
    }
    private void move()
    {
        //Make a new 'velocity' for train
        m_moveSpeed = new Vector2(m_Speed, m_rb.velocity.y);
        
        m_rb.velocity = m_moveSpeed;
        //moving speed toward max speed in the specified time...
        m_Speed = Mathf.SmoothDamp(m_Speed, m_MaxSpeed, ref m_cVelocity, m_SmoothTime);
    }
}

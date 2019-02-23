using UnityEngine;

public class TriggerBlackhole : MonoBehaviour
{
    //for cinematic:
    public float m_TimeWait = 5;
    private PlayerController playerScript;
    public GameObject vCam1;
    public GameObject vCam2;

    public static bool Entered { get; private set;}
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerScript = other.GetComponent<PlayerController>();
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Entered = true;
        vCam1.SetActive(false);
        vCam2.SetActive(true);
        playerScript.enabled = false;
    }
    private void Update()
    {
        if (!Entered || playerScript.enabled)
            return;

        //after waiting 5 second cinematic
        if (m_TimeWait <= 0)
        {
            playerScript.enabled = true;
            vCam1.SetActive(true);
            vCam2.SetActive(false);
        }
        m_TimeWait -= Time.deltaTime;
    }
}

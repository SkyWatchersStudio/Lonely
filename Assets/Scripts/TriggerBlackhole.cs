using UnityEngine;

public class TriggerBlackhole : MonoBehaviour
{
    //for cinematic:
    public float m_TimeWait = 5;
    private PlayerController playerScript;
    //Virtual cameras game object...
    public GameObject vCam1, vCam2;

    public static bool Entered { get; private set;}
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerScript = other.GetComponent<PlayerController>();
        //Stop the player for cinematic...
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Entered = true;
        DisablerCam();
        playerScript.enabled = false;
    }
    //Active and DeActivate vcams:
    void DisablerCam()
    {
        //if the object is active deactivate it else active it...
        vCam1.SetActive(!vCam1.activeSelf);
        vCam2.SetActive(!vCam2.activeSelf);
    }
    private void Update()
    {
        if (!Entered)
            return;

        //after waiting 5 second cinematic
        if (m_TimeWait <= 0)
        {
            playerScript.enabled = true;
            DisablerCam();

            gameObject.SetActive(false);
        }
        m_TimeWait -= Time.deltaTime;
    }
}

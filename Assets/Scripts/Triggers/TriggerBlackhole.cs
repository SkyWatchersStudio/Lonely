using UnityEngine;

public class TriggerBlackhole : EventTriggers
{
    //for cinematic:
    public float m_TimeWait = 5;
    private PlayerController playerScript;

    public static bool Entered { get; private set;}
    public override void OnTriggerEnter2D(Collider2D other)
    {
        playerScript = other.GetComponent<PlayerController>();

        Entered = true;
        playerScript.enabled = false;
    }
    public override void OnTriggerStay2D(Collider2D other) {}
    public override void OnTriggerExit2D() {}
    private void Update()
    {
        if (!Entered || playerScript.enabled)
            return;

        //after waiting 5 second cinematic
        if (m_TimeWait <= 0)
            playerScript.enabled = true;
        m_TimeWait -= Time.deltaTime;
    }
}

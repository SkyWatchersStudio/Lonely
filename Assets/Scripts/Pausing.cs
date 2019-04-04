using UnityEngine;

public class Pausing : MonoBehaviour
{
    private Camera m_mainCamera;
    private void Awake()
    {
        m_mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        //set position...
        transform.position = m_mainCamera.transform.position;
        //bring sprite in front of camera...
        transform.position -= Vector3.forward * -1;
        //back everything to normal
        Time.timeScale = 0;
    }
    //bring frames to normal...
    private void OnDisable() => Time.timeScale = 1;
}

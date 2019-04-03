using UnityEngine;

public class Pausing : MonoBehaviour
{
    private float m_width;
    private float m_height;
    private Camera m_mainCamera;

    private void Awake()
    {
        m_width = Screen.width;
        m_height = Screen.height;
        m_mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        //set position...
        transform.position = m_mainCamera.transform.position;
        transform.position -= Vector3.forward * -1;
        

        //test:
        Vector3 scale = transform.localScale;
        scale = scale / Screen.width;
        transform.localScale = scale;
        

        //change the scale of background pause:
        transform.localScale += new Vector3(Screen.width / m_width, Screen.height / m_height);

        //back everything to normal
        Time.timeScale = 0;
        m_width = Screen.width;
        m_height = Screen.height;
    }
    private void OnDisable()
    {
        //bring frames to normal
        Time.timeScale = 1;
    }
}

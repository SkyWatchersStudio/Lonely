using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject m_PauseCanvas;
    
    private bool m_isPause = false;

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !m_isPause)
        {
            //no frame allowed to come...
            Time.timeScale = 0;
            m_PauseCanvas.SetActive(true); //canvas that made for pause
            m_isPause = true;
        }
        else if (Input.GetButtonDown("Pause") && m_isPause)
        {
            m_PauseCanvas.SetActive(false);
            Time.timeScale = 1;
            m_isPause = false;
        }
    }
    public void QuiteTheGame()
    {
        Application.Quit();
    }
}

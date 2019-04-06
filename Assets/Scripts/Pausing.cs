using UnityEngine;

public class Pausing : MonoBehaviour
{
    public AudioSource m_pauseAudioSource;
    private void OnEnable() 
    {
        m_pauseAudioSource.Play();
    }
    public void QuitTheGame() 
    {
        Application.Quit();
    }
}

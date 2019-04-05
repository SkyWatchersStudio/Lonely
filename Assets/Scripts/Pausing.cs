using UnityEngine;

public class Pausing : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnMouseDown() 
    {
        Application.Quit();
    }
    //bring frames to normal...
    private void OnDisable() => Time.timeScale = 1;
}

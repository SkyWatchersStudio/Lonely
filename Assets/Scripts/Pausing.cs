using UnityEngine;

public class Pausing : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = Camera.main.transform.position;
        transform.position -= Vector3.forward * -1;
        Time.timeScale = 0;
    }
    private void OnMouseDown()
    {
        Application.Quit();
    }
    private void OnDisable() 
    {
        Time.timeScale = 1;
    }
}

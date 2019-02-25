using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        if (!TriggerBlackhole.Entered)
            return;
        
        transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime);        
    }
}

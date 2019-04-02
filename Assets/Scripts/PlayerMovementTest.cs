using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private void Update() 
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * 10 * Time.deltaTime * moveInput;
    }
}
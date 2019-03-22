using UnityEngine;

public class CollisionBreak : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

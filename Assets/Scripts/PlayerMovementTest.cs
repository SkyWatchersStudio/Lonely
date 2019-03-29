using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(moveInput * Time.deltaTime * 10, 0));
    }
    private void OnDrawGizmos() 
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, collider.size);
    }
}

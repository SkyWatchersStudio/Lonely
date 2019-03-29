using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private void Update() 
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(moveInput * Time.deltaTime * 10, 0));
    }
}

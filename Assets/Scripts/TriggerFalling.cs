using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFalling : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player")
        {
            Debug.Log("Player entered...");
        }
    }
}

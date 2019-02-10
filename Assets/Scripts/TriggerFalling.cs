using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerFalling : MonoBehaviour
{
    public BoxCollider2D[] m_boxColliders;
    public float m_wait = 2;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        ColliderDesiabler();
    }
    void ColliderDesiabler()
    {
        foreach (BoxCollider2D box in m_boxColliders)
        {
            box.isTrigger = true;
            Debug.Log($"{box} is trigger.");
            
            yield return new WaitForSeconds(m_wait);
        }
    }
}

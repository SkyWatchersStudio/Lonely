using UnityEngine;
using System.Collections;

public class TriggerFalling : MonoBehaviour
{
    public BoxCollider2D[] m_boxColliders;
    public float m_wait = 2;

    void OnTriggerEnter2D()
    {
        ColliderDesiabler();
    }
    void ColliderDesiabler()
    {
        // StartCoroutine(TimeWaiter());
        int i = 0;
        m_boxColliders[i++].isTrigger = true;
        print($"{m_boxColliders[i]} is disabled");
        
        if (i < m_boxColliders.Length)
        {
            ColliderDesiabler();
        }
    }
    IEnumerator TimeWaiter()
    {
        yield return (new WaitForSeconds(m_wait));
    }
}

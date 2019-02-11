using UnityEngine;
using System.Collections;

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
        int i = 1;
        foreach (BoxCollider2D box in m_boxColliders)
        {
            StartCoroutine(WaitTime());
            
            box.isTrigger = true;
            Debug.Log($"box {i} is trigger now.");
            i++;
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(m_wait);
    }
}

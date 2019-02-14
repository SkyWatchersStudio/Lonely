using UnityEngine;
using System.Collections;

public class TriggerFalling : MonoBehaviour
{
    public BoxCollider2D[] m_boxColliders;
    public float m_wait = 2;
    private int i = 0;
    void OnTriggerEnter2D()
    {
        //Repeats calling function until cancel invoke.
        //first m_wait is for first waiting and second m_wait is for every call waiting...
        InvokeRepeating("ColliderDisabler", m_wait, m_wait);
    }
    void ColliderDisabler()
    {
        m_boxColliders[i++].enabled = false;
        print($"{i} is disabled");

        if (i == m_boxColliders.Length)
        {
            CancelInvoke("ColliderDisabler");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlackhole : MonoBehaviour
{    
    public int m_TimeWait = 5;

    private void OnTriggerEnter2D()
    {
        Entered = true;
    }

    public static bool Entered { get; private set;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlackhole : MonoBehaviour
{    
    private void OnTriggerEnter2D() => Entered = true;

    public static bool Entered { get; private set;}
}

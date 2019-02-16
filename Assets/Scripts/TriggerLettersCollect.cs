using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLettersCollect : MonoBehaviour
{
    public float xRect = 400;
    public float yRect = 100;
    public float widthRect = 50;
    public float heightRect = 50;
    
    private bool checkEnterance = false;

    private void OnTriggerEnter2D() {
        checkEnterance = true;
    }
    private void OnTriggerExit2D() {
        checkEnterance = false;
    }
    private void OnGUI() {
        Rect guiPos;
        
        if (checkEnterance) {
            guiPos  = new Rect(xRect, yRect, widthRect, heightRect);
        }
    }
}

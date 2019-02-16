using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLettersCollect : MonoBehaviour
{
    public float xRect = 25;
    public float yRect = 25;
    public float widthRect = 100;
    public float heightRect = 30;
    public string textField = "Hello, World!";
    
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

            textField = GUI.TextField (guiPos, textField);
        }
    }
}

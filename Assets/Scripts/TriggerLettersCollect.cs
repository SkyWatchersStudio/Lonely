using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLettersCollect : MonoBehaviour
{
    public Rect[] buttonPos;
    public Rect box;
    
    private float n = 56.42f;
    private string[] letters;
    private bool checkEnterance = false;
    private int clicked, firstClicked;
    private string boxString, value;

    private void OnTriggerEnter2D() {
        letters = new string[4] {"e", "l", "l", "a"};
        clicked = 0;
        boxString = "ella?";
        checkEnterance = true;
    }
    private void OnTriggerExit2D() => checkEnterance = false;
        
    private void OnGUI() {        
        if (checkEnterance) 
        {
            GUI.Box(box, boxString);
            
            if (GUI.Button(buttonPos[0], letters[0]))
                OnClick(0);
            if (GUI.Button(buttonPos[1], letters[1]))
                OnClick(1);
            if (GUI.Button(buttonPos[2], letters[2]))
                OnClick(2);
            if (GUI.Button(buttonPos[3], letters[3]))
                OnClick(3);
        }
    }
    private void OnClick(int num)
    {
        clicked++;
        if (!clicked.Equals(2))
        {
            firstClicked = num;
            return;
        }
        
        string value = letters[num];
        letters[num] = letters[firstClicked];
        letters[firstClicked] = value;

        clicked = 0;

        boxString = letters[0] + letters[1] + letters[2] + letters[3] + "?";
    }
}

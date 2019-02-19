using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLettersCollect : MonoBehaviour
{
    //Positioning the GUI elements
    public Rect[] buttonPos;
    public Rect box;

    private float n = 56.42f;
    private string[] letters;
    private bool checkEnterance = false;
    private int clicked, firstClicked;
    private string boxString, value;

    //reset everything on enter trigger again:
    private void OnTriggerEnter2D() {
        letters = new string[4] {"e", "l", "l", "a"};
        clicked = 0;
        boxString = "ella?";
        checkEnterance = true;
    }
    private void OnTriggerExit2D() => checkEnterance = false;

    //The actuall representation goes here:
    private void OnGUI() {
        if (checkEnterance)
        {
            //a box in buttons background that shows the complete name
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
        //as we want to track our player click so we can swap to button string...
        clicked++;
        if (!clicked.Equals(2))
        {
            //store the value of first click and wait untill second clicked happen
            firstClicked = num;
            return;
        }

        //a temporary variable to swap our values
        string tempVar = letters[num];
        letters[num] = letters[firstClicked];
        letters[firstClicked] = tempVar;

        clicked = 0;

        //update string that shows in box for complete name:
        boxString = letters[0] + letters[1] + letters[2] + letters[3] + "?";
    }
}

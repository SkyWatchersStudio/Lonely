using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLetters : MonoBehaviour
{
    public Rect[] buttonPos;  //Position of the buttons:
    public Rect boxPos;  //Position of the box specificly

    private bool checkEnterance = false;
    private string[] letters;  //The player name in letters
    private string boxText;  //What will show to player of bind letters together
    private int clicked, firstClicked;

    //Reset everything on every enterance:
    private void OnTriggerEnter2D()
    {
        letters = new string[4] { "e", "l", "l", "a" }; //we changing our array so we should reset it
        boxText = "ella?";
        clicked = 0;  //if player clicked one time and left the collider we should reset it....
        checkEnterance = true;
    }

    private void OnTriggerExit2D() => checkEnterance = false;

    //The actuall representation:
    private void OnGUI()
    {
        if (checkEnterance)
        {
            GUI.Box(boxPos, boxText);

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
        if (!clicked.Equals(2))  //if this is frist time player click...
        {
            firstClicked = num;
            return;
        }

        //swaping values this is only way i think and best:
        string temp = letters[num];
        letters[num] = letters[firstClicked];
        letters[firstClicked] = temp;

        clicked = 0; //after every two successfuly click reset click
        boxText = letters[0] + letters[1] + letters[2] + letters[3] + "?";  //update box text the player see
    }
}

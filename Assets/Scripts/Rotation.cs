using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    //*******************************************************/ variables for setting the rotating and moving speed of the dreamcatcher
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    /********************************************************/
    //*******************************************************/ transforms are the targets for moving the dreamcatcher
    public Transform upperTarget;
    public Transform lowerTarget;
    /********************************************************/

    //********************************************************/ timer of moving the dreamcatcher and rotate angle and rotate turn
    private float timer;
    private Vector3 rotate;
    private bool turn;
    /*********************************************************/

    void Start()
    {
        rotate = new Vector3(0.0f,0.0f,1.0f);
        timer = duration;
        turn = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        transform.Rotate(rotate * rotationSpeed * Time.deltaTime);

        if(turn == true)
        {
            transform.position = Vector2.MoveTowards(transform.position,upperTarget.position,speed * Time.deltaTime);
        }
        else if(turn == false)
        {
            transform.position = Vector2.MoveTowards(transform.position,lowerTarget.position,speed * Time.deltaTime);
        }

        checkTurn();
    }

    private void checkTurn()
    {
        if(timer <= 0)
        {
            //could use turn = !turn;
            if(turn == true)
            {
                turn = false;
            }
            else if(turn == false)
            {
                turn = true;
            }
            timer = duration;
        }
    }
}

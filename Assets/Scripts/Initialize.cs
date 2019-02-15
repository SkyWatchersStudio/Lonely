using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    //***********************************************/ setting the destination position and the condition that if the bird reached the target or not
    [SerializeField] private float speed;
    private Vector2 startPos;
    private Rigidbody2D rb;
    private bool reach;
    /************************************************/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = new Vector2(transform.position.x,transform.position.y + 2);
    }

    void Update()
    {
        checkReach();
        if(reach == false)
        {
            rb.velocity = Vector2.up * speed;
        }
        else if(reach == true)
        {
            rb.velocity = Vector2.zero;
            GetComponent<BirdController>().enabled = true;
            Destroy(this);
        }  
    }

    private void checkReach()
    {
        if(transform.position.y < startPos.y)
        {
            reach = false;
        }
        else
        {
            reach = true;
        }
    }
}

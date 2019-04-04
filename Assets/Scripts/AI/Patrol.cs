using UnityEngine;
using StateStuff;

public class Patrol : state<AI> 
{
    private static Patrol instance;

    private Patrol()
    {
        if(instance != null)
            return;
        
        instance = this;
    }

    public static Patrol Instance
    {
        get
        {
            if(instance == null)
                new Patrol();

            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        owner.gameObject.GetComponent<Renderer>().material.color = Color.white;
        if(owner.transform.localScale.x > 0)
        {
            owner.speed = Mathf.Abs(owner.speed);
        }
        else if(owner.transform.localScale.x < 0)
        {
            owner.speed = -Mathf.Abs(owner.speed);
        }
    }

    public override void ExitState(AI owner)
    {

    }

    public override void UpdateState(AI owner)
    {
        if ( owner.ray.collider )
        {
            Vector2 sc = owner.transform.localScale;
            sc.x *= -1.0f;
            owner.rayDirection.x *= -1.0f;
            owner.transform.localScale = sc;
            owner.speed *= -1.0f;
        }
    }

    public override void FixedUpdateState(AI owner)
    {
        owner.rb.velocity = new Vector2(owner.speed,owner.rb.velocity.y);
    }

}

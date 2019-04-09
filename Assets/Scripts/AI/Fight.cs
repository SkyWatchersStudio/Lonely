using UnityEngine;
using StateStuff;

public class Fight : state<AI>
{
    private static Fight instance;

    private Fight()
    {
        if(instance != null)
            return;

        instance = this;
    }

    public static Fight Instance
    {
        get
        {
            if(instance == null)
                new Fight();

            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        owner.rb.velocity = Vector2.zero;
        owner.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public override void ExitState(AI owner)
    {

    }
    
    public override void UpdateState(AI owner)
    {
        if ( owner.transform.position.x > owner.player.position.x && owner.speed > 0.0f )
        {
            Vector2 sc = owner.transform.localScale;
            sc.x *= -1.0f;
            owner.transform.localScale = sc;
            owner.speed = -Mathf.Abs(owner.speed);
        }
        else if ( owner.transform.position.x < owner.player.position.x && owner.speed < 0.0f )
        {
            Vector2 sc = owner.transform.localScale;
            sc.x *= -1.0f;
            owner.transform.localScale = sc;
            owner.speed = Mathf.Abs(owner.speed);
        }

        owner.colForAtt = Physics2D.OverlapCircle( owner.laserPos.position, owner.attRadius, owner.playerLayer );
        owner.ray = Physics2D.Raycast( owner.laserPos.position, owner.rayDirection, owner.distance, owner.wallLayer );

        if ( owner.colForAtt )
        {
            if ( owner.transform.localScale.x > 0.0f )
            {
                owner.colForAtt.SendMessage( "hurt", 1.0f );
            }
            else if ( owner.transform.localScale.x < 0.0f )
            {
                owner.colForAtt.SendMessage( "hurt", -1.0f );
            }
        }

        if ( owner.ray && owner.colForAtt == null )
            owner.rb.velocity = Vector2.up * owner.jumpForce;
    }

    public override void FixedUpdateState(AI owner)
    { 
        owner.rb.velocity = new Vector2(owner.speed,owner.rb.velocity.y);
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Enemy
{

private bool onPlayerScreen;
private System.Random randomGenerator;
    public override void Initialize(int layerId, System.Random randomGenerator)
    {
        base.Initialize(layerId,randomGenerator);
        this.randomGenerator = randomGenerator;
        Speed = 2.01f;
        onPlayerScreen = false;
        moving = false;
        hp =1;

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if(onPlayerScreen && Vector2.Distance(player.position, transform.position) < 5){
            moving = true;
        }
        if(moving && transform.position.y > maxBorder)
        {
            Destroy(gameObject);

        }
    }


    private void FixedUpdate()
    {
        if(moving)
        {

        rb.MovePosition((Vector2)transform.position + ((Vector2)transform.up * Time.fixedDeltaTime) * Speed);
        }
    }

    private void OnBecameVisible() {
        onPlayerScreen = true;    
    }

    private void OnBecameInvisible() {
        onPlayerScreen = false;
    }

    private void OnCollisionEnter2D(Collision2D other){
            Destroy(gameObject);
        
    }

   


}

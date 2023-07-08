using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class Enemy : MonoBehaviour
{
    protected Transform player;
    protected float minBorder;
    protected float maxBorder;
    protected float screenHeight;
    private const float maxDif = 3;
    protected Rigidbody2D rb;
    protected float Speed;
    protected bool moving;
    protected float hp;

    public virtual void Initialize(int layerId, System.Random randomGenerator)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layerId;
        screenHeight = Camera.main.ViewportToWorldPoint(new Vector2(1,1)).y *2;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = 1;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
                rb.isKinematic =true;

        rb.gravityScale = 0;
    }

    protected virtual void LateUpdate()
    {
        minBorder = player.position.y - maxDif;
        maxBorder = player.position.y + screenHeight + maxDif;
        if (transform.position.y < minBorder )
        {
            Destroy(gameObject);
        }
    }

     protected void OnParticleCollision(GameObject other) {
        Debug.Log($"Enemy {gameObject.name} got hit from {other.name}");
        hp--;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}

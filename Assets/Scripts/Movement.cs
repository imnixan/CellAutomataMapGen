
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IShooter
{
    private const float Speed = 2;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private Vector2 Direction;
    private Shooter shooter;
    private float screenHalfWidth;
    private float lefPower,
        rightPower;

    void Start()
    {
        screenHalfWidth = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.isKinematic =true;
        ps = GetComponentInChildren<ParticleSystem>();
        shooter = gameObject.AddComponent<Shooter>();
        shooter.StartShoot(1.5f, this);
    }

    public void MoveLeft()
    {
        lefPower = -1;
    }

    public void MoveRight()
    {
        rightPower = 1;
    }

    public void StopMovingLeft()
    {
        lefPower = 0;
    }

    public void StopMovingRight()
    {
        rightPower = 0;
    }

    private void FixedUpdate()
    {
        Direction = transform.up;
        Direction.x = lefPower + rightPower;
        float potentialX = (Direction + (Vector2)transform.position).x;
        if(Mathf.Abs(potentialX) > 15 - screenHalfWidth)
        {
            Direction.x = 0;
            
        }
        rb.MovePosition((Vector2)transform.position + (Direction * Time.fixedDeltaTime) * Speed);
    }

    public void Shoot(){
        ps.Emit(1);
    }

     protected void OnParticleCollision(GameObject other) {
       Debug.Log($"ParticleHit {other.name}");
    }
}

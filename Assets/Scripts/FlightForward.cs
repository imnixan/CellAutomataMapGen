using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightForward : MonoBehaviour
{
    private const float Speed = 2;
    private Rigidbody2D rb;
    private Vector2 Direction;
    private float screenHalfWidth;
    private float lefPower,
        rightPower;

    void Start()
    {
        Direction = Vector2.up;
        screenHalfWidth = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
        Direction.x = lefPower + rightPower;
        rb.MovePosition((Vector2)transform.position + (Direction * Time.fixedDeltaTime) * Speed);
        if (transform.position.x < -15 + screenHalfWidth)
        {
            transform.position = new Vector2(-15 + screenHalfWidth, transform.position.y);
        }
        if (transform.position.x > 15 - screenHalfWidth)
        {
            transform.position = new Vector2(15 - screenHalfWidth, transform.position.y);
        }
    }
}

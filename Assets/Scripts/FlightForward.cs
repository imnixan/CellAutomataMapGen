using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightForward : MonoBehaviour
{
    private const float Speed = 2;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + Vector2.up * Time.fixedDeltaTime * Speed);
    }
}

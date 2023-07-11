using System;

using UnityEngine;

public class Car : Enemy
{
    private bool onPlayerScreen;
    private System.Random randomGenerator;
    private const int maxRotation = 1;

    public override void Initialize(int layerId, System.Random randomGenerator)
    {
        base.Initialize(layerId, randomGenerator);
        this.randomGenerator = randomGenerator;
        Speed = 2.1f;
        onPlayerScreen = false;
        moving = false;
        hp = 1;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (onPlayerScreen && Vector2.Distance(player.position, transform.position) < 5)
        {
            moving = true;
        }
        if (moving && transform.position.y > maxBorder)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.MovePosition(
                (Vector2)transform.position + ((Vector2)transform.up * Time.fixedDeltaTime) * Speed
            );
        }
    }

    private void OnBecameVisible()
    {
        onPlayerScreen = true;
    }

    private void OnBecameInvisible()
    {
        onPlayerScreen = false;
    }

    private void OnEnable()
    {
        Movement.PlayerShot += OnPlayerShot;
    }

    private void OnDisable()
    {
        Movement.PlayerShot -= OnPlayerShot;
    }

    private void OnPlayerShot()
    {
        Shoot();
    }

    public override void Shoot()
    {
        if (moving && onPlayerScreen)
        {
            int xRot,
                yRot;
            xRot = randomGenerator.Next(-maxRotation, maxRotation + 1);
            yRot = randomGenerator.Next(-maxRotation, maxRotation + 1);
            transform.up += new Vector3(xRot, yRot, 0);
            if (MathF.Abs(xRot) > 0 || MathF.Abs(yRot) > 0)
            {
                sm.PlaySound(action);
            }
        }
    }
}

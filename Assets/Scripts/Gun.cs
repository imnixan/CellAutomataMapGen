using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Enemy, IShooter
{

    private Shooter shooter;
    private ParticleSystem gun;

    private void Start()
    {
        shooter = gameObject.AddComponent<Shooter>();
        gun = GetComponentInChildren<ParticleSystem>();
    }
    private void LateUpdate()
    {
        transform.up = transform.position - player.transform.position;
    }
       private void OnCollisionEnter2D(Collision2D other) {
            Destroy(gameObject); 
    }

     public void Shoot()
    {
        gun.Emit(1);
        
    }

    private void OnBecameVisible() {
        shooter.StartShoot(2, this);
    }
    private void OnBecameInvisible()
    {
        shooter.StopShooting();
    }


}

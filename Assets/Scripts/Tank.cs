
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy, IShooter
{
    private Transform tower;
    private Vector2 Direction;
    private System.Random randomGenerator;
    private ParticleSystem ps;
    private ParticleSystem gun;
    private Shooter shooter;
    //вывести корутину в отдельный объект, а коллбек будет переписываемым шутом
    private const int maxRotation = 1;
    public override void Initialize(int layerId, System.Random randomGenerator)
    {
        base.Initialize(layerId, randomGenerator);
        ps = GetComponentInChildren<ParticleSystem>();
        ps.GetComponent<ParticleSystemRenderer>().sortingOrder = layerId + 1;
        Speed =1;
        this.randomGenerator = randomGenerator;
        moving = false;
        tower = transform.GetChild(0);
        tower.GetComponent<SpriteRenderer>().sortingOrder = layerId +1;
        shooter = gameObject.AddComponent<Shooter>();
        gun = tower.GetComponentInChildren<ParticleSystem>();
        hp =2 ;

    }

    protected override void LateUpdate(){
        tower.up = player.transform.position - tower.position;
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            rb.MovePosition((Vector2)transform.position + ((Vector2)transform.up * Time.fixedDeltaTime) * Speed);
        }
    }

    public void Shoot()
    {
       transform.up += new Vector3(randomGenerator.Next(-maxRotation,maxRotation + 1),
         randomGenerator.Next(-maxRotation, maxRotation  + 1),0);
        gun.Emit(1);
        
    }

    private void OnBecameVisible() {
        moving = true;
        shooter.StartShoot(1, this);
        ps.Play();
    }
    private void OnBecameInvisible()
    {
        shooter.StopShooting();
        ps.Stop();
    }
}

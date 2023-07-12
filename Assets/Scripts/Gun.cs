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

    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.up = transform.position - player.transform.position;
    }

    public override void Shoot()
    {
        base.Shoot();
        gun.Emit(1);
    }

    private void OnBecameVisible()
    {
        shooter.StartShoot(3, this);
    }

    private void OnBecameInvisible()
    {
        shooter.StopShooting();
    }
}

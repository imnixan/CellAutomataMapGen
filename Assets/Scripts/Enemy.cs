using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected AudioClip action;
    protected SoundManager sm;
    protected Transform player;
    protected float minBorder;
    protected float maxBorder;
    protected float screenHeight;
    protected const float maxDif = 3;
    protected Rigidbody2D rb;
    protected float Speed;
    protected bool moving;
    protected float hp;

    public static event UnityAction<Enemy, Vector3, bool> EnemyDestroy;

    public virtual void Initialize(int layerId, System.Random randomGenerator)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layerId;
        screenHeight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y * 2;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = 1;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.isKinematic = true;

        rb.gravityScale = 0;
        sm = GameObject.FindGameObjectWithTag("Player").GetComponent<SoundManager>();
    }

    protected virtual void LateUpdate()
    {
        minBorder = player.position.y - maxDif;
        maxBorder = player.position.y + screenHeight + maxDif;
        if (transform.position.y < minBorder)
        {
            Destroy(gameObject);
        }
    }

    protected void OnParticleCollision(GameObject other)
    {
        if (DecreaseHp())
        {
            EnemyDestroy?.Invoke(this, transform.position, true);
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (DecreaseHp())
        {
            EnemyDestroy?.Invoke(this, transform.position, false);
            Destroy(gameObject);
        }
    }

    protected bool DecreaseHp()
    {
        hp--;
        return hp <= 0;
    }

    public virtual void Shoot()
    {
        sm.PlaySound(action);
    }
}

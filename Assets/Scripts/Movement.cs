using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI rocketsLeftUi;

    [SerializeField]
    AudioClip shoot;
    public static event UnityAction<Vector3> PlayerDestroy;
    public static event UnityAction PlayerShot;
    private const float Speed = 2;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private Vector2 Direction;
    private int _rocketsCounter;
    private SoundManager sm;

    private int RocketsCounter
    {
        get { return _rocketsCounter; }
        set
        {
            _rocketsCounter = value;
            rocketsLeftUi.text = _rocketsCounter.ToString();
        }
    }
    private bool alive;

    private float potentialX;
    private float screenBorder;

    private Vector2 screenSize;
    private float lefPower,
        rightPower;

    private float rotAngle = 20;
    private float playerAngle;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Init()
    {
        sm = GetComponent<SoundManager>();

        screenSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Camera.main.orthographic = false;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.isKinematic = true;
        ps = GetComponentInChildren<ParticleSystem>();
        RocketsCounter = 20;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerHpCounter>().Init();
        alive = true;
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
        if (alive)
        {
            Move();
        }
    }

    private void Move()
    {
        Direction = transform.up;
        Direction.x = lefPower + rightPower;
        potentialX = (Mathf.Abs((Direction + (Vector2)transform.position).x));
        playerAngle = rotAngle * Direction.x * -1;
        screenBorder = 15 - screenSize.x;

        if (potentialX > screenBorder)
        {
            Direction.x = 0;
            playerAngle = 0;
        }
        rb.MovePosition((Vector2)transform.position + (Direction * Time.fixedDeltaTime) * Speed);
        transform.eulerAngles = new Vector3(0, playerAngle, 0);
    }

    public void Shoot()
    {
        if (RocketsCounter > 0 & alive)
        {
            RocketsCounter--;
            PlayerShot?.Invoke();
            ps.Emit(1);
            sm.PlaySound(shoot);
        }
    }

    public void Dead()
    {
        PlayerDestroy?.Invoke(transform.position);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerHpCounter>().enabled = false;
        this.enabled = false;
    }

    public void Fall()
    {
        StartCoroutine(Falling());
    }

    private IEnumerator Falling()
    {
        float localScale = transform.localScale.x;
        while (localScale > 1)
        {
            localScale -= 0.05f;
            transform.localScale = new Vector2(localScale, localScale);
            yield return new WaitForSeconds(0.1f);
        }
        Dead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ammo"))
        {
            RocketsCounter += 10;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Oil"))
        {
            StopAllCoroutines();
            Destroy(other.gameObject);
            transform.localScale = new Vector2(1.5f, 1.5f);
        }
    }
}

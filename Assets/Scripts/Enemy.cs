using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Transform player;
    protected float CamBot;

    public virtual void Initialize(int layerId)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layerId;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void LateUpdate()
    {
        CamBot = Camera.main.ViewportToWorldPoint(Vector2.zero).y - 1;
        if (transform.position.y < CamBot)
        {
            Destroy(gameObject);
        }
    }
}

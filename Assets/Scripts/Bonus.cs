using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void LateUpdate()
    {
        float minBorder = player.position.y - 2;

        if (transform.position.y < minBorder)
        {
            Destroy(gameObject);
        }
    }
}

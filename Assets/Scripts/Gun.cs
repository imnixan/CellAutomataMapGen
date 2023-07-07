using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Enemy
{
    private void Update()
    {
        transform.up = transform.position - player.transform.position;
    }
}

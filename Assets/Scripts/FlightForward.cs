using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightForward : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + Vector3.up,
            0.05f
        );
    }
}

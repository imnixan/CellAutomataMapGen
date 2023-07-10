
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeReference] Transform player;
    private Vector3 dif;
    private void Start() {
        dif = transform.position - player.position;
    }


    private void LateUpdate()
    {
        transform.position = player.position + dif;
    }
}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Sprite[] frames;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        foreach (var sprite in frames)
        {
            yield return new WaitForSeconds(0.1f);
            sr.sprite = sprite;
        }
        Destroy(gameObject);
    }
}

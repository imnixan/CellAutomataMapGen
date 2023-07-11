using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private AudioClip sound;
    private SoundManager sm;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("Player").GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        Enemy.EnemyDestroy += OnEnemyDestroy;
        Movement.PlayerDestroy += OnPlayerDestroy;
    }

    private void OnDisable()
    {
        Enemy.EnemyDestroy -= OnEnemyDestroy;
        Movement.PlayerDestroy -= OnPlayerDestroy;
    }

    void OnEnemyDestroy(Enemy type, Vector3 pos, bool player)
    {
        MakeExplosion(pos);
    }

    void OnPlayerDestroy(Vector3 pos)
    {
        sm.Vibrate();
        MakeExplosion(pos);
    }

    private void MakeExplosion(Vector3 pos)
    {
        sm.PlaySound(sound);
        Instantiate(explosion, pos, new Quaternion());
    }
}

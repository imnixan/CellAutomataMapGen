using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour {
    

    private IEnumerator Shooting(float time, IShooter shooter){
        while(true){
            shooter.Shoot();
            yield return new WaitForSeconds(time);
        }
    }

    public void StartShoot(float time, IShooter shooter){
        StartCoroutine(Shooting(time, shooter));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    
}

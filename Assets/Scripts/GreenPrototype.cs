using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPrototype : MonoBehaviour
{
    public float LifeEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet1"))
        {
            LifeEnemy -= 5f;
            if(LifeEnemy<=0)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("PistolBullet"))
        {
            LifeEnemy -= 30f;
            if (LifeEnemy <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("SgBullet"))
        {
            LifeEnemy -= 5f;
            if (LifeEnemy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

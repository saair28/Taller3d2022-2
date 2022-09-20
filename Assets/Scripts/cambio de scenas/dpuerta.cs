using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dpuerta : MonoBehaviour
{
    public GameObject puerta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet1")
        {
            Destroy(this.gameObject);
            Destroy(puerta);
        }

    }
}


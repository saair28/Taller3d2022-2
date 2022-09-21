using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activecollicions : MonoBehaviour
{
    veri veri;
  //  prueba prueba;
    private void Start()
    {
   //     prueba = puerta.GetComponent<prueba>();
        veri = GameObject.Find("verificador").GetComponent<veri>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PistolBullet")
        {
            veri.a = veri.a - 1;
          //  prueba.nenemigos = prueba.nenemigos-1;
            Destroy(this.gameObject);
        }
    }


}

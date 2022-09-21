using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
 
    public GameObject puerta;
    public int nenemigos;
    public Renderer rend;
    public void Start()
    {
        // dest = GameObject.Find("puerta1").GetComponent<destruir>();
        rend = puerta.GetComponent<Renderer>();
    }
    private void Update()
    {
        abrirz();
    }   
    public void abrirz()
    {
        if (nenemigos == 0)
        {
            puerta.gameObject.SetActive (false);
         //   Destroy(this.gameObject);
            //  rend.enabled  = false;   
        }
    }
   
}

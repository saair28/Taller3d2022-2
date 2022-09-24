using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class veri : MonoBehaviour
{
    public bool enemi = true;
    public GameObject puerta;
    public int a;

    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {      
    }
    void Update()
    {
        //a = enemies.Count;
        abrirz();
    }
    public void abrirz()
    {
        if (a == 0)
        {  
            if (enemi == false & a == 0)
            {
                puerta.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //if(other.gameObject.CompareTag("enemigo1"))   
        //{
        //    enemi = false;
        //    a = a + 1;
        //}
    }
}

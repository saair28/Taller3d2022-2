using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponchange : MonoBehaviour
{
    PlayerWeapons PlayerWeapons;
    public GameObject trompeta;
    public  GameObject casta�uelas;
    public   GameObject triangulo;
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            trompeta.SetActive(true);
            casta�uelas.SetActive(false);
            triangulo.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            trompeta.SetActive(false);
            casta�uelas.SetActive(true);
            triangulo.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            trompeta.SetActive(false);
            casta�uelas.SetActive(false);
            triangulo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

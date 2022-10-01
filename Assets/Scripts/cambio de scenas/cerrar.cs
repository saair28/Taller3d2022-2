using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cerrar : MonoBehaviour
{
   [SerializeField] private GameObject botonPausa;
   [SerializeField] private GameObject menuPausa;
    public void Cerrar()
    {
        Application.Quit();
    }
}

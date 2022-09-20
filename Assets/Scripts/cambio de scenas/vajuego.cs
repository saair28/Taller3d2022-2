using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vajuego : MonoBehaviour
{
   public void EscenaJuego()
    {
        SceneManager.LoadScene("brady_scene");
    }
    public void CargarNivel(string nombredescena)
    {
        SceneManager.LoadScene(nombredescena);
    }
}

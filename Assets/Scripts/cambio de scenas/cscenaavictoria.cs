using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class cscenaavictoria : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("BalaVERDE"))
        {
            SceneManager.LoadScene(2);
        }
        if (col.CompareTag("BalaROJA"))
        {
            SceneManager.LoadScene(2);
        }
        if (col.CompareTag("BalaAZUL"))
        {
            SceneManager.LoadScene(2);
        }
    }
}

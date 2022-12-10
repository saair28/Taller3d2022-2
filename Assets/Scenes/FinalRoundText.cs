using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoundText : MonoBehaviour
{
    public ScenarioManager manager;
    public Animator anim;
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    
    void Update()
    {
        FinalTexto();
    }
    void FinalTexto()
    {
        if(manager.currentRound > 14)
        {
            anim.SetTrigger("Activado");
        }
    }
}

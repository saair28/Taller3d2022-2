using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public ScenarioManager manager;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivacionLava();
    }
    void ActivacionLava()
    {
        if(manager.currentRound > 14)
        {
            anim.SetTrigger("LavaActivacion");
        }
    }
}

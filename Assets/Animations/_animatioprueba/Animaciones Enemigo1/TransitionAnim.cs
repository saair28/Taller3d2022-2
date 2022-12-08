using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnim : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("p"))
        {
            anim.SetBool("Move", true);
        }
        if (!Input.GetKey("p"))
        {
            anim.SetBool("Move", false);
        }


        if (Input.GetKey("space"))
        {
            anim.SetBool("Attack", true);
        }
        if (!Input.GetKey("space"))
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKey("x"))
        {
            anim.SetBool("Damage", true);
        }
        if (!Input.GetKey("x"))
        {
            anim.SetBool("Damage", false);
        }

        if (Input.GetKey("z"))
        {
            anim.SetBool("Death", true);
            Destroy(gameObject,2f);
        }
    }
}

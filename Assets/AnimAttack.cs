using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAttack : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {

        if(this.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Move", false);
            anim.SetBool("Attack", true);
        }

    }

}

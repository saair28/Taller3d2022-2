using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Fadeout",1);
    }

    // Update is called once per frame
    public void Fadeout()
    {
        animator.Play("Animation1");
    }
}

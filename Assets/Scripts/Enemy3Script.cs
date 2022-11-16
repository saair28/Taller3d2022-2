using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Script : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public Animator anim;
    public float moveSpeed;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.isActiveAndEnabled)
            agent.SetDestination(player.transform.position);

    }
}

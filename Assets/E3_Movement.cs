using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E3_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public enum EnemyStates
    {
        moving,
        attacking,
    }
    public EnemyStates enemyStates;

    public NavMeshAgent agent;
    public GameObject player;
    public Animator anim;
    public float moveSpeed;
    public float minHeight, maxHeight;
    public GameObject enemyBody;

    public bool isAttacking = false;
    float f = 0;
    float t = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        
        switch (enemyStates)
        {
            case (EnemyStates.moving):
                //LookPlayer();
                agent.enabled = true;
                rb.isKinematic = true;
                agent.SetDestination(player.transform.position);

                f = 0;
                t += Time.deltaTime * 1.5f;
                enemyBody.transform.localPosition = new Vector3(enemyBody.transform.localPosition.x, Mathf.Lerp(minHeight, maxHeight, t), enemyBody.transform.localPosition.z);

                break;
            case (EnemyStates.attacking):
                //isAttacking = true;
                agent.enabled = false;
                rb.isKinematic = false;
                rb.velocity = transform.forward * moveSpeed * 300 * Time.deltaTime;
                t = 0;
                f += Time.deltaTime *1.5f;
                enemyBody.transform.localPosition = new Vector3(enemyBody.transform.localPosition.x, Mathf.Lerp(maxHeight, minHeight, f), enemyBody.transform.localPosition.z);
                break;
        }      
    }

    void LookPlayer()
    {
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Vector3 playerPosition;
    public NavMeshAgent agent;
    public int life;

    public int valor;

    void Start()
    {
        //life = 80;
        agent = GetComponent<NavMeshAgent>();
        //if(GetComponent<E3_Movement2>() != null)
        //    agent.speed = GetComponent<E3_Movement2>().moveSpeed;
        GetComponent<Enemy>().agent.enabled = true;
    }

    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(agent != null && agent.isActiveAndEnabled)
            agent.SetDestination(playerPosition);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}

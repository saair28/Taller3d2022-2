using UnityEngine;
using UnityEngine.AI;

public class FasterEnemy : MonoBehaviour
{
    public Transform target;
    public Vector3 playerPosition;
    NavMeshAgent agent;
    public int life;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        life = 120;
    }

    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        agent.SetDestination(playerPosition);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}

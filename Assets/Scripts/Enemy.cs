using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Vector3 playerPosition;
    public NavMeshAgent agent;
    public int life;

    public int valor;

    public int enemyType;
    // 1 = Rápido
    // 2 = Lento
    // 3 = Volador

    //public GameObject targetPlayer;
    public Animator anim;

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
        anim.SetBool("Move", true);

        /*if (Vector3.Distance(transform.position, targetPlayer.transform.position) < 4)

        {
            Debug.Log("Activado");
            anim.SetBool("Idle", true);
            agent.enabled = false;
            anim.SetBool("Move", false);
        }
        else
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (agent != null && agent.isActiveAndEnabled)
                agent.SetDestination(playerPosition);
            anim.SetBool("Idle", false);
            agent.enabled = true;
            anim.SetBool("Move", true);
        }
    }*/

        void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
        // holis
    }
}

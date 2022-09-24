using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    public Transform target;
    //public int life;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 position = target.position;
        agent.destination = position;
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    /*
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
        {
            // Aquí, en lugar de poner un valor como "1", habría que usar una variable que indique el daño del enemigo.
            // Eso se haría jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.

            life = life - 1;

            if (life == 0)
            {
                Destroy(gameObject);
            }

        }

    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E3_Movement2 : MonoBehaviour
{
    public Rigidbody rb;
    public enum EnemyStates
    {
        moving,
        attacking,
    }
    public EnemyStates enemyStates;

    //public NavMeshAgent agent;
    //public GameObject player;
    public Animator anim;
    public float moveSpeed, attackingSpeed;
    public float distanceToAttack;
    public float normalHeight, doorHeight, attackingHeight;
    public GameObject enemyBody;

    public float transitionDuration;

    public bool isAttacking = false;
    public bool changingAltitude = false;

    public bool nearDoor = false;
    public bool passingThrough = false;

    public float bodyAltitude = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        GetComponent<Enemy>().agent.speed = moveSpeed;

        bodyAltitude = enemyBody.transform.position.y;

        //GetComponent<Enemy>().agent.enabled = true;
        rb.isKinematic = true;
        
    }

    void Update()
    {
        if (Vector3.Distance(GetComponent<Enemy>().playerPosition, transform.position) <= distanceToAttack)
        {
            enemyStates = EnemyStates.attacking;
        }
        else
        {
            enemyStates = EnemyStates.moving;
        }

        //agent.SetDestination(player.transform.position);
        anim.SetBool("nearPlayer", Vector3.Distance(GetComponent<Enemy>().playerPosition, transform.position) <= distanceToAttack);
        switch (enemyStates)
        {
            case (EnemyStates.moving):
                //LookPlayer();
                if (isAttacking && !changingAltitude)
                {
                    StartCoroutine(ChangeAltitude(normalHeight));
                }
                if(!isAttacking && nearDoor && !passingThrough && !changingAltitude)
                {
                    StartCoroutine(ChangeAltitude2(doorHeight));
                }
                if(!nearDoor && passingThrough && !changingAltitude)
                {
                    StartCoroutine(ChangeAltitude2(normalHeight));
                }
                GetComponent<Enemy>().agent.enabled = true;
                rb.isKinematic = true;
                GetComponent<Enemy>().agent.speed = moveSpeed;
                //enemyBody.transform.position = new Vector3(enemyBody.transform.position.x, normalHeight, enemyBody.transform.position.z);

                break;

            case (EnemyStates.attacking):
                if(!isAttacking && !changingAltitude)
                {
                    StartCoroutine(ChangeAltitude(attackingHeight));
                }
                //agent.enabled = false;
                rb.isKinematic = false;
                GetComponent<Enemy>().agent.speed = attackingSpeed;
                //if(isAttacking)
                //    enemyBody.transform.position = new Vector3(enemyBody.transform.position.x, attackingHeight, enemyBody.transform.position.z);
                //rb.velocity = transform.forward * moveSpeed * 300 * Time.deltaTime;
                break;
        }
    }

    IEnumerator ChangeAltitude(float to)
    {
        changingAltitude = true;
        float percent = 0;
        float timeFactor = 1 / transitionDuration;
        float Yvalue = 0;
        //t = t * t * (3f - 2f * t);
        while(percent < 1)
        {
            percent += Time.deltaTime * timeFactor;
            Yvalue = Mathf.SmoothStep(bodyAltitude, to, percent);
            enemyBody.transform.localPosition = new Vector3(enemyBody.transform.localPosition.x, Yvalue, enemyBody.transform.localPosition.z);
            yield return null;
        }

        if (!isAttacking)
            isAttacking = true;
        else
            isAttacking = false;

        bodyAltitude = enemyBody.transform.localPosition.y;
        changingAltitude = false;
    }

    IEnumerator ChangeAltitude2(float to)
    {
        changingAltitude = true;
        float percent = 0;
        float timeFactor = 1 / transitionDuration;
        float Yvalue = 0;
        //t = t * t * (3f - 2f * t);
        while (percent < 1)
        {
            percent += Time.deltaTime * timeFactor;
            Yvalue = Mathf.SmoothStep(bodyAltitude, to, percent);
            enemyBody.transform.localPosition = new Vector3(enemyBody.transform.localPosition.x, Yvalue, enemyBody.transform.localPosition.z);
            yield return null;
        }

        if (!passingThrough)
            passingThrough = true;
        else passingThrough = false;

        bodyAltitude = enemyBody.transform.localPosition.y;
        changingAltitude = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Door"))
        {
            nearDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            nearDoor = false;
        }
    }
}

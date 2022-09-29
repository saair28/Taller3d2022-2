using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Script_Attack : MonoBehaviour
{
    Vector3 playerPos;
    public float distanceToAttack;

    public GameObject enemyBody;
    public float prepareAttackForce;
    public float attackingForce;
    //public float recoveryVel;

    public bool isAttacking = false;
    public bool backingUp = false;

    public float attackCD;
    float attackCDCounter = 0;
    Vector3 initialBodyPos;

    public float elapsedTime;
    public float waitTimeGoBack;
    void Start()
    {
        //enemyBody.GetComponent<Rigidbody>()
        attackCDCounter = attackCD;
        initialBodyPos = enemyBody.transform.localPosition;
        enemyBody.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        GetComponent<Enemy3Script>().anim.SetBool("isAttacking", isAttacking);
        attackCDCounter -= Time.deltaTime;

        //if(isAttacking)
        //{
        //    //Vector3.forward * GetComponent<Enemy3Script>().moveSpeed * Time.deltaTime;
        //    transform.position = Vector3.MoveTowards(transform.position, playerPos, GetComponent<Enemy3Script>().moveSpeed*3 * Time.deltaTime);
        //}

        playerPos = GetComponent<Enemy3Script>().player.transform.position;
        if(Vector3.Distance(transform.position, playerPos) < distanceToAttack && !isAttacking && attackCDCounter <= 0)
        {
            StartCoroutine(Attack());
            attackCDCounter = attackCD;
        }
    }

    public IEnumerator Attack()
    {
        //Debug.Log(enemyBody.transform.localPosition);
        enemyBody.GetComponent<Rigidbody>().isKinematic = false;
        isAttacking = true;
        Vector3 _pos = (playerPos - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, _pos, GetComponent<Enemy3Script>().moveSpeed * Time.deltaTime);
        Vector3 pos = (playerPos - enemyBody.transform.position).normalized;
        GetComponent<NavMeshAgent>().enabled = false;
        enemyBody.GetComponent<Rigidbody>().AddForce(-pos * prepareAttackForce);
        yield return new WaitForSeconds(0.15f);
        enemyBody.GetComponent<Rigidbody>().AddForce(pos * attackingForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.7f);
        enemyBody.GetComponent<Rigidbody>().velocity = Vector2.zero;
        GetComponent<NavMeshAgent>().enabled = true;
        while (elapsedTime < waitTimeGoBack)
        {
            enemyBody.transform.localPosition = Vector3.Lerp(enemyBody.transform.localPosition, initialBodyPos, (elapsedTime / waitTimeGoBack));
            elapsedTime += Time.deltaTime*5;
            Debug.Log(elapsedTime);
            yield return null;
        }
        isAttacking = false;
        enemyBody.GetComponent<Rigidbody>().isKinematic = true;
        elapsedTime = 0;
    }
}

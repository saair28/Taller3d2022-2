using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_Attack : MonoBehaviour
{
    E3_Movement enemyMove;
    public float distanceToAttack;
    public bool isAttacking = false;
    //public bool nearPlayer = false;
    public GameObject enemyBody;

    public float originalBodyYPos;

    void Start()
    {
        enemyMove = GetComponent<E3_Movement>();
    }

    void Update()
    {
        if(Vector3.Distance(enemyMove.player.transform.position, transform.position) <= distanceToAttack)
        {
            if(enemyMove.enemyBody.transform.localPosition.y == enemyMove.maxHeight)
                enemyMove.enemyStates = E3_Movement.EnemyStates.attacking;            
            //if (!isAttacking)
            //{
            //    Attack();
            //    isAttacking = true;
            //}
        }
        else
        {
            enemyMove.enemyStates = E3_Movement.EnemyStates.moving;
        }
    }

    public void Attack()
    {
        Debug.Log("AUIDAD");
    }
}

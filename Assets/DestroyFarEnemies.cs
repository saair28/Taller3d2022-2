using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestroyFarEnemies : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject furthestEnemy;
    public GameObject[] spawnPoints;
    public GameObject closestSpawn;
    public float distance;

    public float timeToCheck;
    public float timerCount;

    void Start()
    {
        timerCount = timeToCheck;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Update()
    {
        if (enemyList.Count >= 1)
        {
            timerCount -= Time.deltaTime;
            if (timerCount <= 0)
            {
                GetFurthestEnemy(enemyList);
                timerCount = timeToCheck;
            }

            if (furthestEnemy != null && Vector3.Distance(transform.position, furthestEnemy.transform.position) >= distance)
            {
                Debug.Log("ENEMIGO LEJANO DESTRUIDO");
                furthestEnemy.GetComponent<NavMeshAgent>().enabled = false;
                furthestEnemy.transform.position = GetClosestSpawn(spawnPoints).position;
                furthestEnemy.GetComponent<NavMeshAgent>().enabled = true;
                furthestEnemy = null;
                //nearestEnemy = null;
                //enemyList.Remove(nearestEnemy);
                //Destroy(nearestEnemy);
                //nearestEnemy = null;
                //ScenarioManager.instance.enemiesOnScreen--;
                //ScenarioManager.instance.enemiesThisRound--;
                //if (ScenarioManager.instance.allEnemiesSpawned)
                //{
                //    ScenarioManager.instance.allEnemiesSpawned = false;
                //}
            }
        }
        
    }
    void GetFurthestEnemy(List<GameObject> enemies)
    {
        //furthestEnemy = null;
        //float minDist = Mathf.Infinity;
        float minDist = 0;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist > minDist)
            {
                furthestEnemy = t;
                minDist = dist;
            }
        }

        Debug.Log("Enemigo más lejano: " + furthestEnemy);
        //return nearestEnemy.transform;
    }

    Transform GetClosestSpawn(GameObject[] spawns)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in spawns)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        closestSpawn = tMin;
        return closestSpawn.transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawn : MonoBehaviour
{
    public GameObject roomSpawn;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Player"))
        //{
        //    if(roomSpawn.GetComponent<SpawnEnemies>().enemiesLeft <= 0)
        //        roomSpawn.GetComponent<SpawnEnemies>().Spawn();
        //    //if (!roomSpawn.GetComponent<SpawnEnemies>().startSpawning)
        //    //    roomSpawn.GetComponent<SpawnEnemies>().startSpawning = true;
        //    //else roomSpawn.GetComponent<SpawnEnemies>().startSpawning = false;
        //}
    }
}

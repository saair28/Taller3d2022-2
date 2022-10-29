using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public GameObject[] roomDoors;
    //public MeshRenderer[] roomMesh;
    public Transform[] spawnPositions;
    public BoxCollider boxCol;
    public bool unlocked = false;

    public GameObject[] enemyPool;

    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = true;
    }

    public void OpenRoom()
    {
        unlocked = true;
        boxCol.enabled = true;
        if (roomDoors != null)
        {
            for(int i = 0; i < roomDoors.Length; i++)
            {
                roomDoors[i].SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("ROOM");
            ScenarioManager.instance.currentRoom = this.gameObject;
            SpawnEnemies.instance.UpdateSpawnPositions();
            //FindObjectOfType<ScenarioManager>().currentRoom = gameObject;
            //FindObjectOfType<SpawnEnemies>().UpdateSpawnPositions();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScenarioManager.instance.currentRoom = this.gameObject;
            SpawnEnemies.instance.UpdateSpawnPositions();
            Debug.Log("FUNCIONA");
            //FindObjectOfType<ScenarioManager>().currentRoom = gameObject;
            //FindObjectOfType<SpawnEnemies>().UpdateSpawnPositions();
        }
    }
}

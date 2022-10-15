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
        boxCol.enabled = false;
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
            FindObjectOfType<ScenarioManager>().currentRoom = gameObject;
            FindObjectOfType<SpawnEnemies>().UpdateSpawnPositions();
        }
    }
}

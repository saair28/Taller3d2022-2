using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public GameObject roomDoor;
    //public MeshRenderer[] roomMesh;
    public Transform[] spawnPositions;
    public BoxCollider[] boxCol;
    public bool unlocked = false;

    //public GameObject[] enemyPool;

    void Start()
    {
        boxCol = GetComponents<BoxCollider>();
        for(int i = 0; i< boxCol.Length; i++)
        {
            boxCol[i].enabled = true;
        }
        
    }

    public void OpenRoom()
    {
        unlocked = true;
        for (int i = 0; i < boxCol.Length; i++)
        {
            boxCol[i].enabled = true;
        }
        if (roomDoor != null)
        {
            roomDoor.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawner;
    public Transform spawnPos;

    int randomInt;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SpawnRandom();
        }
    }

    void SpawnRandom()
    {
        randomInt = Random.Range(0, spawner.Length);
       GameObject holis =  Instantiate(spawner[randomInt], spawnPos.position, spawnPos.rotation);
        //   FindObjectOfType<veri>().enemies.Add(holis);
        FindObjectOfType<veri>().a++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public int enemyNumber;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPositions;
    public Material[] materials;
    //
    //
    //
    public bool roundStarted = false;

    public int enemiesLeft = 0;
    public TextMeshProUGUI enemyCounterText;

    public GameObject door;

    void Start()
    {
        spawnPositions = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if(enemiesLeft <= 0 && roundStarted)
        {
            door.SetActive(false);
        }
    }

    public void Spawn()
    {
        roundStarted = true;
        for (int i = 0; i < enemyNumber; i++)
        {
            int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomNumberPosition = Random.Range(0, spawnPositions.Length);
            int randomNumberColor = Random.Range(0, materials.Length);
            //0 = AZUL
            //1 = ROJO
            //2 = VERDE
            GameObject go = Instantiate(enemyPrefabs[randomNumberEnemy], spawnPositions[randomNumberPosition].position, Quaternion.identity);
            go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
            go.GetComponent<EnemyHealth>().enemyColor = randomNumberColor;
            enemiesLeft++;
            UpdateEnemyCounter();
        }
    }

    public void UpdateEnemyCounter()
    {
        enemyCounterText.text = "Enemigos: " + enemiesLeft.ToString();
    }
}

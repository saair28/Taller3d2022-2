using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    //public int enemyNumber;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPositions;
    public Material[] materials;

    public float spawnTime;
    public bool isSpawning = false;

    public int enemiesLeft = 0;
    public TextMeshProUGUI enemyCounterText;
    public TextMeshProUGUI roundCounterText;
    public TextMeshProUGUI betweenRoundsCounterText;

    void Start()
    {
        spawnPositions = GetComponent<ScenarioManager>().rooms[0].GetComponent<RoomScript>().spawnPositions;
    }

    void Update()
    {
        // SOLO PARA EL PROTOTIPO
        //if(enemiesLeft <= 0 && roundStarted)
        //{
        //    door.SetActive(false);
        //}
    }

    public IEnumerator SpawnE()
    {
        isSpawning = true;
        //for (int i = 0; i < numberOfEnemies; i++)
        //{
        //    int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
        //    int randomNumberPosition = Random.Range(0, spawnPositions.Length);
        //    int randomNumberColor = Random.Range(0, materials.Length);
        //    //0 = AZUL
        //    //1 = ROJO
        //    //2 = VERDE
        //    GameObject go = Instantiate(enemyPrefabs[randomNumberEnemy], spawnPositions[randomNumberPosition].position, Quaternion.identity);
        //    go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
        //    go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
        //    enemiesLeft++;
        //    UpdateEnemyCounter();
        //    yield return new WaitForSeconds(spawnTime);
        //}

        int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
        int randomNumberPosition = Random.Range(0, spawnPositions.Length);
        int randomNumberColor = Random.Range(0, materials.Length);
        //0 = AZUL
        //1 = ROJO
        //2 = VERDE
        GameObject go = Instantiate(enemyPrefabs[randomNumberEnemy], spawnPositions[randomNumberPosition].position, Quaternion.identity);
        go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
        go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
        enemiesLeft++;
        UpdateEnemyCounter();
        yield return new WaitForSeconds(spawnTime);

        isSpawning = false;
    }

    //public void Spawn()
    //{
    //    for (int i = 0; i < enemyNumber; i++)
    //    {
    //        int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
    //        int randomNumberPosition = Random.Range(0, spawnPositions.Length);
    //        int randomNumberColor = Random.Range(0, materials.Length);
    //        //0 = AZUL
    //        //1 = ROJO
    //        //2 = VERDE
    //        GameObject go = Instantiate(enemyPrefabs[randomNumberEnemy], spawnPositions[randomNumberPosition].position, Quaternion.identity);
    //        go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
    //        go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
    //        enemiesLeft++;
    //        UpdateEnemyCounter();
    //    }
    //}

    public void UpdateEnemyCounter()
    {
        enemyCounterText.text = "Enemigos: " + enemiesLeft.ToString();
    }

    public void UpdateRoundCounter()
    {
        roundCounterText.text = "Ronda: " + GetComponent<ScenarioManager>().currentRound.ToString();
    }

    public void UpdateSpawnPositions()
    {
        spawnPositions = GetComponent<ScenarioManager>().currentRoom.GetComponent<RoomScript>().spawnPositions;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

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

    public int valorTotal;
    

    void Start()
    {
        spawnPositions = GetComponent<ScenarioManager>().rooms[0].GetComponent<RoomScript>().spawnPositions;

        for (int i = 0; i < FindObjectOfType<RoomScript>().enemyPool.Length; i++)
        {
            valorTotal += FindObjectOfType<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor;
        }
        
    }

    void Update()
    {
        // SOLO PARA EL PROTOTIPO
        //if(enemiesLeft <= 0 && roundStarted)
        //{
        //    door.SetActive(false);
        //}
    }

    public void EnemyChance()
    {
        int randomValor = Random.Range(0, valorTotal);
        int randomNumberPosition = Random.Range(0, spawnPositions.Length);
        int randomNumberColor = Random.Range(0, materials.Length);

        for (int i = 0; i < FindObjectOfType<RoomScript>().enemyPool.Length; i++)
        {
            if (randomValor < FindObjectOfType<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor)
            {
                GameObject go = Instantiate(FindObjectOfType<RoomScript>().enemyPool[i], spawnPositions[randomNumberPosition].position, Quaternion.identity);
                go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
                go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
                return;
            }
            randomValor -= FindObjectOfType<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor;
        }
    }     

    public IEnumerator SpawnE()
    {
        isSpawning = true;

        int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
        //int randomNumberPosition = Random.Range(0, spawnPositions.Length);
        //int randomNumberColor = Random.Range(0, materials.Length);

        EnemyChance();

        //0 = AZUL
        //1 = ROJO
        //2 = VERDE
        //GameObject go = Instantiate(enemyPrefabs[randomNumberEnemy], spawnPositions[randomNumberPosition].position, Quaternion.identity);
        //go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
        //go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
        enemiesLeft++;
        UpdateEnemyCounter();
        yield return new WaitForSeconds(spawnTime);

        isSpawning = false;
    }

    public void SpawnETutorial()
    {
        isSpawning = true;

        //0 = AZUL
        //1 = ROJO
        //2 = VERDE
        GameObject go1 = Instantiate(FindObjectOfType<TutorialScript>().enemyPrefabTUTORIAL, FindObjectOfType<TutorialScript>().spawnPositionsTUTORIAL[0].position, Quaternion.identity);
        go1.GetComponent<MeshRenderer>().material = materials[0];
        go1.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)0;
        go1.GetComponent<Enemy>().agent = go1.GetComponent<NavMeshAgent>();
        go1.GetComponent<Enemy>().agent.speed = 0;
        enemiesLeft++;
        GameObject go2 = Instantiate(FindObjectOfType<TutorialScript>().enemyPrefabTUTORIAL, FindObjectOfType<TutorialScript>().spawnPositionsTUTORIAL[1].position, Quaternion.identity);
        go2.GetComponent<MeshRenderer>().material = materials[1];
        go2.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)1;
        go2.GetComponent<Enemy>().agent = go2.GetComponent<NavMeshAgent>();
        go2.GetComponent<Enemy>().agent.speed = 0;
        enemiesLeft++;
        UpdateEnemyCounter();

        isSpawning = false;
    }

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

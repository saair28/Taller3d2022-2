using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public static SpawnEnemies instance;
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

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void EnemyChance()
    {
        int randomValor = Random.Range(0, valorTotal);
        int randomNumberPosition = Random.Range(0, spawnPositions.Length);

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (randomValor < enemyPrefabs[i].GetComponent<Enemy>().valor)
            {
                GameObject go = Instantiate(enemyPrefabs[i], spawnPositions[randomNumberPosition].position, Quaternion.identity);
                go.GetComponent<Enemy>().agent = GetComponent<NavMeshAgent>();
                FindObjectOfType<DestroyFarEnemies>().enemyList.Add(go);

                int randomNumberColor = 0;

                if (go.GetComponent<Enemy>().enemyType == 1) // RÁPIDO
                {
                    randomNumberColor = Random.Range(0, 3);
                    go.GetComponentInChildren<MeshRenderer>().material = materials[randomNumberColor];
                   // go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
                    go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
                }
                else if(go.GetComponent<Enemy>().enemyType == 2) // LENTO
                {
                    randomNumberColor = Random.Range(3, 6);
                    go.GetComponentInChildren<MeshRenderer>().material = materials[randomNumberColor];
                    //go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
                    go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor - 3;
                }
                else // VOLADOR
                {
                    randomNumberColor = Random.Range(6, 9);
                    go.GetComponentInChildren<MeshRenderer>().material = materials[randomNumberColor];
                    //go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
                    go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor - 6;
                }
                
                return;
            }
            randomValor -= enemyPrefabs[i].GetComponent<Enemy>().valor;
        }
    }     

    public IEnumerator SpawnE()
    {
        isSpawning = true;


        EnemyChance();

        //0 = AZUL
        //1 = ROJO
        //2 = VERDE
        enemiesLeft++;
        GetComponent<ScenarioManager>().enemiesThisRound++;
        UpdateEnemyCounter();
        yield return new WaitForSeconds(spawnTime);

        isSpawning = false;
    }

    public void UpdateEnemyCounter()
    {
        enemyCounterText.text = "Enemies: " + enemiesLeft.ToString();
    }

    public void UpdateRoundCounter()
    {
        roundCounterText.text = "Round: " + GetComponent<ScenarioManager>().currentRound.ToString();
      
    }

    public void UpdateSpawnPositions()
    {
        spawnPositions = GetComponent<ScenarioManager>().currentRoom.GetComponent<RoomScript>().spawnPositions;
    }
}

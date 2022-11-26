using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

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
        //spawnPositions = GetComponent<ScenarioManager>().rooms[0].GetComponent<RoomScript>().spawnPositions;

        //for (int i = 0; i < ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool.Length; i++)
        //{
        //    valorTotal += ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor;
        //}
        
    }

    void Update()
    {

    }

    public void EnemyChance()
    {
        int randomValor = Random.Range(0, valorTotal);
        int randomNumberPosition = Random.Range(0, spawnPositions.Length);

        //for (int i = 0; i < ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool.Length; i++)
        //{
        //    if (randomValor < ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor)
        //    {
        //        GameObject go = Instantiate(ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool[i], spawnPositions[randomNumberPosition].position, Quaternion.identity);
        //        go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
        //        go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
        //        return;
        //    }
        //    randomValor -= ScenarioManager.instance.currentRoom.GetComponent<RoomScript>().enemyPool[i].GetComponent<Enemy>().valor;
        //}
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
                
                //go.GetComponent<MeshRenderer>().material = materials[randomNumberColor];
                //go.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)randomNumberColor;
                
                return;
            }
            randomValor -= enemyPrefabs[i].GetComponent<Enemy>().valor;
        }
    }     

    public IEnumerator SpawnE()
    {
        //if (GetComponent<ScenarioManager>().enemiesOnScreen < GetComponent<ScenarioManager>().maxEnemiesOnScreen)
        //{ 
        //}
        isSpawning = true;

        //int randomNumberEnemy = Random.Range(0, enemyPrefabs.Length);
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
        GetComponent<ScenarioManager>().enemiesThisRound++;
        UpdateEnemyCounter();
        yield return new WaitForSeconds(spawnTime);

        isSpawning = false;
    }

    //public void SpawnETutorial()
    //{
    //    isSpawning = true;

    //    //0 = AZUL
    //    //1 = ROJO
    //    //2 = VERDE
    //    GameObject go1 = Instantiate(FindObjectOfType<TutorialScript>().enemyPrefabTUTORIAL, FindObjectOfType<TutorialScript>().spawnPositionsTUTORIAL[0].position, Quaternion.identity);
    //    go1.GetComponent<MeshRenderer>().material = materials[0];
    //    go1.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)0;
    //    go1.GetComponent<Enemy>().agent = go1.GetComponent<NavMeshAgent>();
    //    go1.GetComponent<Enemy>().agent.speed = 0;
    //    enemiesLeft++;
    //    GameObject go2 = Instantiate(FindObjectOfType<TutorialScript>().enemyPrefabTUTORIAL, FindObjectOfType<TutorialScript>().spawnPositionsTUTORIAL[1].position, Quaternion.identity);
    //    go2.GetComponent<MeshRenderer>().material = materials[1];
    //    go2.GetComponent<EnemyHealth>().enemyColor = (EnemyHealth.EnemyColor)1;
    //    go2.GetComponent<Enemy>().agent = go2.GetComponent<NavMeshAgent>();
    //    go2.GetComponent<Enemy>().agent.speed = 0;
    //    enemiesLeft++;
    //    UpdateEnemyCounter();

    //    isSpawning = false;
    //}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ScenarioManager : MonoBehaviour
{
    
    public static ScenarioManager instance;
    public NavMeshSurface navMeshSurface;
    public int currentRound = 0;
    //public int numberOfEnemies;
    public GameObject currentRoom;
    public List<GameObject> rooms = new List<GameObject>();

    [Header("Enemy values")]
    public int enemiesOnScreen = 0;
    public int maxEnemiesOnScreen = 5;
    public int enemiesThisRound = 0;
    public int totalEnemiesForTheRound = 0;
    public bool allEnemiesSpawned = false;

    public bool roundStarting = false;
    public bool roundInProgress = false;

    public float timeBetweenRounds;
    [HideInInspector] public float timeBetweenRoundsCounter;

    public int[] milestones;

    public int buffMenuRounds;
    public int buffMenuRoundsLast = 0;

    public Transform[] bossSpawns;
    public GameObject[] bossPrefabs;

    private void Awake()
    {
        instance = this;
        currentRoom = rooms[0];
    }
    private void Start()
    {
        
        navMeshSurface = GetComponent<NavMeshSurface>();
        if (!GameManager.tutorial)
        {
            currentRound = 1;
            //GetComponent<TutorialScript>().enabled = false;
        }
        ResetTimer();
        StartCoroutine(UpdateNavMesh(1));
        GetComponent<SpawnEnemies>().UpdateRoundCounter();

        //buffMenuRounds = 1;

        AudioManager.instance.PlaySFXWithDelay(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[0], 1f);

        //Valores iniciales de los enemigos
        SpawnEnemies.instance.enemyPrefabs[0].GetComponent<Enemy>().valor = 70; //LENTO
        SpawnEnemies.instance.enemyPrefabs[1].GetComponent<Enemy>().valor = 30; //RÁPIDO
        SpawnEnemies.instance.enemyPrefabs[2].GetComponent<Enemy>().valor = 0; //VOLADOR

        SpawnEnemies.instance.valorTotal = 0;
        for (int i = 0; i < SpawnEnemies.instance.enemyPrefabs.Length; i++)
        {
            SpawnEnemies.instance.valorTotal += SpawnEnemies.instance.enemyPrefabs[i].GetComponent<Enemy>().valor;
        }

        
    }

    private void Update()
    {
        enemiesOnScreen = GetComponent<SpawnEnemies>().enemiesLeft;
        
        

        if (!GetComponent<TutorialScript>().isActiveAndEnabled) //&& currentRound <= 19)
        {
            if (!roundStarting && !roundInProgress)
            {
                UpdateTotalEnemies();
                if (timeBetweenRoundsCounter <= 0)
                {
                    NextRound();
                    roundStarting = true;
                }
                GoTimer();
            }
            else
            {
                HideTimer();
                GetComponent<BuffMenu>().buffMenuActivated = false;
            }


            if (roundStarting)
            {                
                if(!allEnemiesSpawned)
                {
                    if (enemiesThisRound < totalEnemiesForTheRound)
                    {
                        allEnemiesSpawned = false;
                        if (enemiesOnScreen < maxEnemiesOnScreen)
                            StartCoroutine(GetComponent<SpawnEnemies>().SpawnE());
                    }
                    else
                    {
                        allEnemiesSpawned = true;

                        roundStarting = false;
                        roundInProgress = true;
                    }
                }
            }

            if (roundInProgress && allEnemiesSpawned && GetComponent<SpawnEnemies>().enemiesLeft <= 0)
            {
                //NextRound();
                //if ((currentRound) % 3 == 0 && !GetComponent<BuffMenu>().buffMenuActivated)
                //{
                //    StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
                //}
                if(GetComponent<BuffMenu>().counter >= buffMenuRounds && !GetComponent<BuffMenu>().buffMenuActivated)
                {
                    Debug.Log("BUFFFFFFFFFFFFFF");
                    StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
                    //buffMenuRounds += buffMenuRounds;
                }
                roundInProgress = false;
                allEnemiesSpawned = false;
            }
        }
         
    }

    void UpdateTotalEnemies()
    {
        if (currentRound > 3 && currentRound <= 6)
        {
            totalEnemiesForTheRound = 16;
            maxEnemiesOnScreen = 6;
        }
        else if (currentRound > 7 && currentRound <= 9)
        {
            totalEnemiesForTheRound = 20;
            maxEnemiesOnScreen = 8;
        }
        else if (currentRound > 10 && currentRound <= 12)
        {
            totalEnemiesForTheRound = 24;
            maxEnemiesOnScreen = 10;
        }
        else if(currentRound > 13)
        {
            totalEnemiesForTheRound = 28;
            maxEnemiesOnScreen = 12;
        }
        
    }
    
    

    public void ResetTimer()
    {
        timeBetweenRoundsCounter = timeBetweenRounds;
    }

    public void GoTimer()
    {
        GetComponent<BuffMenu>().FillBar();

        timeBetweenRoundsCounter -= Time.deltaTime;
        ShowTimer();
    }

    public IEnumerator UpdateNavMesh(float time)
    {
        yield return new WaitForSeconds(time);
        navMeshSurface.BuildNavMesh();
    }

    public void LevelProgressionCheck()
    {
        if (currentRound >= milestones[0] && rooms[0].GetComponent<RoomScript>().roomDoor != null) // Se abre la primera puerta
        {
            Destroy(rooms[0].GetComponent<RoomScript>().roomDoor);
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[7], 1f);
            StartCoroutine(UpdateNavMesh(1));
            Debug.Log("Se destruyo 1");
        }
        else if (currentRound >= milestones[1] && rooms[1].GetComponent<RoomScript>().roomDoor != null) // Se abre la segunda puerta
        {
            Destroy(rooms[1].GetComponent<RoomScript>().roomDoor);
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[7], 1f);
            StartCoroutine(UpdateNavMesh(1));
            Debug.Log("Se destruyo 2");

            SpawnEnemies.instance.enemyPrefabs[0].GetComponent<Enemy>().valor = 50; //LENTO
            SpawnEnemies.instance.enemyPrefabs[1].GetComponent<Enemy>().valor = 30; //RÁPIDO
            SpawnEnemies.instance.enemyPrefabs[2].GetComponent<Enemy>().valor = 15; //VOLADOR

            SpawnEnemies.instance.valorTotal = 0;
            for (int i = 0; i < SpawnEnemies.instance.enemyPrefabs.Length; i++)
            {
                SpawnEnemies.instance.valorTotal += SpawnEnemies.instance.enemyPrefabs[i].GetComponent<Enemy>().valor;
            }
        }
        else if (currentRound >= milestones[2] && rooms[2].GetComponent<RoomScript>().roomDoor != null) // Se abre la tercera puerta
        {
            Destroy(rooms[2].GetComponent<RoomScript>().roomDoor);
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[7], 1f);
            StartCoroutine(UpdateNavMesh(1));
            Debug.Log("Se destruyo 3");

            SpawnEnemies.instance.enemyPrefabs[0].GetComponent<Enemy>().valor = 50; //LENTO
            SpawnEnemies.instance.enemyPrefabs[1].GetComponent<Enemy>().valor = 25; //RÁPIDO
            SpawnEnemies.instance.enemyPrefabs[2].GetComponent<Enemy>().valor = 25; //VOLADOR

            SpawnEnemies.instance.valorTotal = 0;
            for (int i = 0; i < SpawnEnemies.instance.enemyPrefabs.Length; i++)
            {
                SpawnEnemies.instance.valorTotal += SpawnEnemies.instance.enemyPrefabs[i].GetComponent<Enemy>().valor;
            }
        }
        else if (currentRound >= milestones[3] && rooms[3].GetComponent<RoomScript>().roomDoor != null) // Se abre la cuarta puerta
        {
            Destroy(rooms[3].GetComponent<RoomScript>().roomDoor);
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[7], 1f);
            StartCoroutine(UpdateNavMesh(1));
            Debug.Log("Se destruyo 4");
        }

        else if(currentRound == 20)
        {
            AudioManager.instance.ChangeMusicBoss();
        }

    }

    public void ShowTimer()
    {
        GetComponent<SpawnEnemies>().betweenRoundsCounterText.gameObject.SetActive(true);
        GetComponent<SpawnEnemies>().betweenRoundsCounterText.text = timeBetweenRoundsCounter.ToString("F2");
    }

    public void HideTimer()
    {
        GetComponent<SpawnEnemies>().betweenRoundsCounterText.gameObject.SetActive(false);
    }

    public void NextRound()
    {
        roundInProgress = false;
        enemiesThisRound = 0;
        UpdateTotalEnemies();
        currentRound++;

        int randomNumber = Random.Range(8, 12);
        if(!AudioManager.instance.sfxSource.isPlaying && (currentRound != milestones[0] || currentRound != milestones[1] || currentRound != milestones[2] || currentRound != milestones[3]))
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[randomNumber], 1f);

        GetComponent<SpawnEnemies>().UpdateRoundCounter();
        LevelProgressionCheck();
        //GetComponent<BuffMenu>().FillBar();
        GetComponent<BuffMenu>().counter++;
        ResetTimer();
    }
}

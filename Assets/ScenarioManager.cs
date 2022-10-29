using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


    private void Awake()
    {
        instance = this;
        currentRoom = rooms[0];
    }
    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        ResetTimer();
        StartCoroutine(UpdateNavMesh(1));
        GetComponent<SpawnEnemies>().UpdateRoundCounter();
    }

    private void Update()
    {
        enemiesOnScreen = GetComponent<SpawnEnemies>().enemiesLeft;

        if (!GetComponent<TutorialScript>().isActiveAndEnabled)
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
                if ((currentRound) % 3 == 0 && !GetComponent<BuffMenu>().buffMenuActivated)
                {
                    StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
                }
                roundInProgress = false;
                allEnemiesSpawned = false;
            }
        }
    }

    void UpdateTotalEnemies()
    {
        if(currentRound > 3 && currentRound <= 6)
        {
            totalEnemiesForTheRound = 16;
        }
        else if (currentRound > 7 && currentRound <= 9)
        {
            totalEnemiesForTheRound = 20;
        }
        else if (currentRound > 10 && currentRound <= 12)
        {
            totalEnemiesForTheRound = 24;
        }
        else totalEnemiesForTheRound = 28;
    }

    public void ResetTimer()
    {
        timeBetweenRoundsCounter = timeBetweenRounds;
    }

    public void GoTimer()
    {
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
        if ((currentRound) % 3 == 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxSource, AudioManager.instance.endRoundSFX, 0.5f);
        }

        if(currentRoom.GetComponent<RoomScript>().roomDoors[0] != null)
        {
            if ((currentRound >= milestones[0]) && currentRoom.GetComponent<RoomScript>().roomDoors[0].activeSelf)
            {
                for (int i = 0; i < currentRoom.GetComponent<RoomScript>().roomDoors.Length; i++)
                {
                    //currentRoom.GetComponent<RoomScript>().roomDoors[i].SetActive(false);
                    //currentRoom.GetComponent<RoomScript>().roomDoors[i].layer = LayerMask.NameToLayer("Ignore Raycast");
                    Destroy(currentRoom.GetComponent<RoomScript>().roomDoors[i]);
                }
                StartCoroutine(UpdateNavMesh(1));
            }
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
        GetComponent<SpawnEnemies>().UpdateRoundCounter();
        LevelProgressionCheck();
        ResetTimer();
        GetComponent<BuffMenu>().FillBar();
    }
}

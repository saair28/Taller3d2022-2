using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;
    public NavMeshSurface navMeshSurface;
    public int currentRound = 0;
    public int numberOfEnemies;
    public GameObject currentRoom;
    public List<GameObject> rooms = new List<GameObject>();

    public bool roundStarting = false;
    public bool roundInProgress = false;

    public float timeBetweenRounds;
    float timeBetweenRoundsCounter;

    public int[] milestones;

    //[Header("Tutorial Things")]
    //public bool tutorial = true;
    //public Transform[] tutorialSpawn;
    //public int numberOfEnemiesTutorial;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        //rooms[0].GetComponent<RoomScript>().boxCol.enabled = true;
        //rooms[0].GetComponent<RoomScript>().unlocked = true;
        timeBetweenRoundsCounter = timeBetweenRounds;
        //currentRound++;
        currentRoom = rooms[0];
        StartCoroutine(UpdateNavMesh(1));
    }

    private void Update()
    {
        if (!roundStarting && !roundInProgress && FindObjectOfType<PlayerWeapons>().currentWeapon != null)
        {
            timeBetweenRoundsCounter -= Time.deltaTime;
            GetComponent<SpawnEnemies>().betweenRoundsCounterText.gameObject.SetActive(true);
            GetComponent<SpawnEnemies>().betweenRoundsCounterText.text = timeBetweenRoundsCounter.ToString("F2");

            if (timeBetweenRoundsCounter <= 0)
            {
                roundStarting = true;
                GetComponent<SpawnEnemies>().UpdateRoundCounter();
                timeBetweenRoundsCounter = timeBetweenRounds;
            }
        }
        else
        {
            GetComponent<SpawnEnemies>().betweenRoundsCounterText.gameObject.SetActive(false);
        }

        if (roundStarting)
        {
            if (GetComponent<SpawnEnemies>().enemiesLeft < numberOfEnemies && !GetComponent<SpawnEnemies>().isSpawning)
                StartCoroutine(GetComponent<SpawnEnemies>().SpawnE());

            if (GetComponent<SpawnEnemies>().enemiesLeft >= numberOfEnemies)
            {
                roundStarting = false;
                roundInProgress = true;
            }
        }

        if (roundInProgress && GetComponent<SpawnEnemies>().enemiesLeft <= 0)
        {
            NextRound();
            LevelProgressionCheck();
        }
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    if (!roundStarting && !roundInProgress)
        //    {
        //        currentRound++;
        //        roundStarting = true;
        //        GetComponent<SpawnEnemies>().UpdateRoundCounter();
        //    }
        //}
    }

    public IEnumerator UpdateNavMesh(float time)
    {
        yield return new WaitForSeconds(time);
        navMeshSurface.BuildNavMesh();
    }
    public void LevelProgressionCheck()
    {
        if (currentRound >= milestones[0])
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxSource, AudioManager.instance.endRoundSFX, 0.5f);
            //if (rooms[1].GetComponent<RoomScript>().unlocked)
            //{
            //    //for (int i = 0; i < GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors.Length; i++)
            //    //{
            //    //    GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors[i].SetActive(false);
            //    //}
            //    //GetComponent<ScenarioManager>().navMeshSurface.BuildNavMesh(); // ESTO VUELVE A BAKEAR EL NAVMESH PARA QUE SE ACTUALICE EL PATHFINDING.
            //    //GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().unlocked = true;
            //    rooms[1].GetComponent<RoomScript>().OpenRoom();
            //    StartCoroutine(UpdateNavMesh(1));
            //    //navMeshSurface.BuildNavMesh(); // ESTO VUELVE A BAKEAR EL NAVMESH PARA QUE SE ACTUALICE EL PATHFINDING.
            //}

        }
    }

    //public void UpdateNavMesh()
    //{
    //    //for(int i = 0; i < currentRoom.GetComponent<RoomScript>().roomMesh.Length; i++)
    //    //{
    //    //    currentRoom.GetComponent<RoomScript>().roomMesh[i].enabled = true;
    //    //}
        
    //    navMeshSurface.BuildNavMesh();

    //    //for (int i = 0; i < currentRoom.GetComponent<RoomScript>().roomMesh.Length; i++)
    //    //{
    //    //    currentRoom.GetComponent<RoomScript>().roomMesh[i].enabled = false;
    //    //}
    //}

    public void NextRound()
    {
        roundInProgress = false;
        currentRound++;
    }

    public void EndRound()
    {

    }
}

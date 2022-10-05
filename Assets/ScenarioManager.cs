using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScenarioManager : MonoBehaviour
{
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

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        rooms[0].GetComponent<RoomScript>().boxCol.enabled = true;
        rooms[0].GetComponent<RoomScript>().unlocked = true;
        timeBetweenRoundsCounter = timeBetweenRounds;
        currentRound++;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    if (!roundStarting && !roundInProgress)
        //    {
        //        currentRound++;
        //        roundStarting = true;
        //        GetComponent<SpawnEnemies>().UpdateRoundCounter();
        //    }
        //}

        if (!roundStarting && !roundInProgress)
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
            if(GetComponent<SpawnEnemies>().enemiesLeft < numberOfEnemies && !GetComponent<SpawnEnemies>().isSpawning)
                StartCoroutine(GetComponent<SpawnEnemies>().SpawnE());

            if(GetComponent<SpawnEnemies>().enemiesLeft >= numberOfEnemies)
            {
                roundStarting = false;
                roundInProgress = true;
            }
        }

        if (roundInProgress && GetComponent<SpawnEnemies>().enemiesLeft <= 0)
        {
            currentRound++;
            roundInProgress = false;
            LevelProgressionCheck();
        }

    }

    public void LevelProgressionCheck()
    {
        if (GetComponent<ScenarioManager>().currentRound >= milestones[0])
        {
            if (!GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().unlocked)
            {
                //for (int i = 0; i < GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors.Length; i++)
                //{
                //    GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors[i].SetActive(false);
                //}
                //GetComponent<ScenarioManager>().navMeshSurface.BuildNavMesh(); // ESTO VUELVE A BAKEAR EL NAVMESH PARA QUE SE ACTUALICE EL PATHFINDING.
                //GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().unlocked = true;
                GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().OpenRoom();
                GetComponent<ScenarioManager>().navMeshSurface.BuildNavMesh(); // ESTO VUELVE A BAKEAR EL NAVMESH PARA QUE SE ACTUALICE EL PATHFINDING.
            }

        }
    }

    public void NextRound()
    {
        currentRound++;
    }

    public void EndRound()
    {

    }
}

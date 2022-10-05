using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelProgression : MonoBehaviour
{
    public int[] roundMilestones;
    void Update()
    {
        //if(GetComponent<ScenarioManager>().currentRound >= roundMilestones[0])
        //{
        //    if(GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors != null)
        //    {
        //        for(int i = 0; i < GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors.Length; i++)
        //        {
        //            GetComponent<ScenarioManager>().rooms[1].GetComponent<RoomScript>().roomDoors[i].SetActive(false);
        //        }
        //        GetComponent<ScenarioManager>().navMeshSurface.BuildNavMesh(); // ESTO VUELVE A BAKEAR EL NAVMESH PARA QUE SE ACTUALICE EL PATHFINDING.
        //    }
            
        //}
    }
}

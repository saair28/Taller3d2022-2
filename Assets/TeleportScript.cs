using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    GameObject player;
    public Transform[] roomCenter;
    //0 -> sala 1;
    //1 -> sala 2;
    //2 -> sala 3;
    //3 -> sala 4;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            player.transform.position = roomCenter[0].position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player.transform.position = roomCenter[1].position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            player.transform.position = roomCenter[2].position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            player.transform.position = roomCenter[3].position;
        }
    }
}

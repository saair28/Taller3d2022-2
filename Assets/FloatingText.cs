using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    //public float destroyTime;
    GameObject player;
    public Vector3 offset;
    public Vector3 randomIntesity;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomIntesity.x, randomIntesity.x),
            Random.Range(-randomIntesity.y, randomIntesity.y), Random.Range(-randomIntesity.z, randomIntesity.z));
    }

    void Update()
    {
        transform.LookAt(player.transform);
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }
}

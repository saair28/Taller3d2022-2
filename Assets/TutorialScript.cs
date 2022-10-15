using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial Things")]
    AudioClip satanVoice;
    public AudioSource audioSource;
    public ScenarioManager scenarioManager;
    public GameObject enemyPrefabTUTORIAL;
    public Transform[] spawnPositionsTUTORIAL;
    public GameObject barricades;

    public GameObject navMeshObject;

    private void Awake()
    {
        scenarioManager.enabled = false;
    }
    void Start()
    {
        satanVoice = (AudioClip)Resources.Load("Audio/SFX/satanVoice");
    }

    void PlaySatanVoice()
    {
        AudioManager.instance.PlaySFX(audioSource, satanVoice, 0.3f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            navMeshObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        if (!ScenarioManager.instance.roundStarting && !ScenarioManager.instance.roundInProgress && FindObjectOfType<PlayerWeapons>().currentWeapon != null)
        {
            PlaySatanVoice();
            ScenarioManager.instance.roundStarting = true;

            if (FindObjectOfType<SpawnEnemies>().enemiesLeft < 2 && !FindObjectOfType<SpawnEnemies>().isSpawning)
            {
                FindObjectOfType<SpawnEnemies>().SpawnETutorial();
            }

            if (FindObjectOfType<SpawnEnemies>().enemiesLeft >= 2)
            {
                ScenarioManager.instance.roundStarting = false;
                ScenarioManager.instance.roundInProgress = true;
            }
        }

        if (ScenarioManager.instance.roundInProgress && FindObjectOfType<SpawnEnemies>().enemiesLeft <= 0)
        {
            //for (int i = 0; i < barricades.Length; i++)
            //{
            //    Destroy(barricades[i]);
            //}
            Destroy(barricades);
            scenarioManager.enabled = true;
            StartCoroutine(scenarioManager.UpdateNavMesh(1));
            //ScenarioManager.instance.NextRound();
            this.enabled = false;
        }


    }
}

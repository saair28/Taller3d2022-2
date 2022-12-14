using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript instance;

    public ScenarioManager scenarioManager;
    public GameObject barricades;
    public bool roundStarted = false;
    public bool buffDone = false;
    //public int currentRound = -1;
    //public bool allEnemiesSpawned = false;

    //public int enemiesOnScreen = 0;
    //public int maxEnemiesOnScreen = 5;
    //public int enemiesThisRound = 0;
    //public int totalEnemiesForTheRound = 0;

    //public float timerBetweenRounds;
    //float timerCount;

    [Header("Tutorial things")]
    public GameObject[] enemyPrefabTUTORIAL;
    public Transform[] spawnPositionsTUTORIAL;
    public GameObject trumpetWeapon, triangleWeapon;
    bool showedOtherWeapons = false;


    [Header("Until round 3 things")]
    public GameObject[] enemyPrefabManual;
    public Transform[] spawnPositions;

    public GameObject navMeshObject;
    bool pickedWeaponVoice = false;
    bool barricadesOffVoice = false;
    bool finishedRound1Voice = false;
    bool finishedRound2Voice = false;
    bool finishedRound3Voice = false;

    private void Awake()
    {
        //scenarioManager.enabled = false;
        instance = this;
    }
    void Start()
    {
        //GetComponent<ScenarioManager>().ResetTimer();
        trumpetWeapon.SetActive(false);
        triangleWeapon.SetActive(false);

        GetComponent<BuffMenu>().buffBarObject.SetActive(false);

        //if(!GameManager.tutorial)
        //{
        //    GetComponent<ScenarioManager>().currentRound = 1;
        //    this.enabled = false;
        //}
    }

    //void ResetTimer()
    //{
    //    timerCount = timerBetweenRounds;
    //}

    //void GoTimer()
    //{
    //    timerCount -= Time.deltaTime;
    //}

    void Update()
    {
        GetComponent<ScenarioManager>().enemiesOnScreen = GetComponent<SpawnEnemies>().enemiesLeft;


        if(GetComponent<ScenarioManager>().currentRound < 1)
        {
            GetComponent<SpawnEnemies>().roundCounterText.text = "Tutorial";
        }
        Tutorial();
    }

    public void Tutorial()
    {
        if(GetComponent<ScenarioManager>().currentRound >= 0)
        {
            if (!showedOtherWeapons)
            {
                trumpetWeapon.SetActive(true);
                triangleWeapon.SetActive(true);

                showedOtherWeapons = true;
            }
        }

        if(GetComponent<ScenarioManager>().currentRound >= 1 && !barricadesOffVoice)
        {
            barricades.SetActive(false);
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[3], 1f);
            barricadesOffVoice = true;
        }

        if(FindObjectOfType<PlayerWeapons>().currentWeapon != null && !pickedWeaponVoice)
        {
            AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[1], 1f);
            pickedWeaponVoice = true;
        }

        if(!roundStarted || !GetComponent<ScenarioManager>().allEnemiesSpawned)
        {
            if (FindObjectOfType<PlayerWeapons>().currentWeapon != null && GetComponent<ScenarioManager>().currentRound == -1)
            {
                //AudioManager.instance.PlaySFX(AudioManager.instance.sfxSource, AudioManager.instance.satanVoice, 0.3f);
                GetComponent<ScenarioManager>().GoTimer();
                if (GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
                {
                    SpawnEnemies_Tutorial();
                    SpawnEnemies.instance.UpdateEnemyCounter();
                    GetComponent<ScenarioManager>().allEnemiesSpawned = true;
                    roundStarted = true;
                    GetComponent<ScenarioManager>().HideTimer();
                }
                
            }
            else if (FindObjectOfType<PlayerWeapons>().weaponList.Count >= 3 && GetComponent<ScenarioManager>().currentRound == 0)
            {
                GetComponent<ScenarioManager>().GoTimer();
                if (GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
                {
                    SpawnEnemies_Tutorial2();
                    SpawnEnemies.instance.UpdateEnemyCounter();
                    GetComponent<ScenarioManager>().allEnemiesSpawned = true;
                    roundStarted = true;
                    GetComponent<ScenarioManager>().HideTimer();
                }
            }
            else if(GetComponent<ScenarioManager>().currentRound == 1)
            {
                barricades.SetActive(false);

                if (!barricades.activeSelf && !updatingNavMesh)
                {
                    GetComponent<BuffMenu>().buffBarObject.SetActive(true);
                    GetComponent<BuffMenu>().counter = 1;
                    UpdateNavMesh();
                }

                GetComponent<ScenarioManager>().totalEnemiesForTheRound = 9;
                GetComponent<ScenarioManager>().GoTimer();
                if (GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
                {
                    if (GetComponent<ScenarioManager>().enemiesThisRound < GetComponent<ScenarioManager>().totalEnemiesForTheRound)
                    {
                        //StartCoroutine(_SpawnEnemies_Round1());
                        //SpawnEnemies_Round1();

                        GetComponent<ScenarioManager>().allEnemiesSpawned = false;
                        SpawnEnemies_Round1();
                    }
                    else
                    {
                        GetComponent<ScenarioManager>().allEnemiesSpawned = true;
                    }

                    roundStarted = true;
                    GetComponent<ScenarioManager>().HideTimer();
                }
            }
            else if (GetComponent<ScenarioManager>().currentRound == 2)
            {
                if(!finishedRound1Voice)
                {
                    AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[4], 1f);
                    finishedRound1Voice = true;
                }

                GetComponent<ScenarioManager>().totalEnemiesForTheRound = 12;
                GetComponent<ScenarioManager>().GoTimer();
                if (GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
                {
                    if (GetComponent<ScenarioManager>().enemiesThisRound < GetComponent<ScenarioManager>().totalEnemiesForTheRound)
                    {
                        //StartCoroutine(_SpawnEnemies_Round2());

                        GetComponent<ScenarioManager>().allEnemiesSpawned = false;
                        SpawnEnemies_Round2();
                    }
                    else
                    {
                        GetComponent<ScenarioManager>().allEnemiesSpawned = true;
                    }
                    //SpawnEnemies_Round2();
                    roundStarted = true;
                    GetComponent<ScenarioManager>().HideTimer();
                }
            }
            else if (GetComponent<ScenarioManager>().currentRound == 3)
            {
                if (!finishedRound2Voice)
                {
                    AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[5], 1f);
                    GetComponent<BuffMenu>().counter = 1;
                    finishedRound2Voice = true;
                }

                GetComponent<ScenarioManager>().totalEnemiesForTheRound = 16;
                GetComponent<ScenarioManager>().GoTimer();

                if (GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
                {
                    if (GetComponent<ScenarioManager>().enemiesThisRound < GetComponent<ScenarioManager>().totalEnemiesForTheRound)
                    {
                        //StartCoroutine(_SpawnEnemies_Round3());
                        GetComponent<ScenarioManager>().allEnemiesSpawned = false;
                        SpawnEnemies_Round3();
                    }
                    else
                    {
                        GetComponent<ScenarioManager>().allEnemiesSpawned = true;
                    }
                    //SpawnEnemies_Round3();
                    roundStarted = true;
                    GetComponent<ScenarioManager>().HideTimer();
                }
            }
            else if(GetComponent<ScenarioManager>().currentRound >= 4)
            {
                if (!finishedRound3Voice)
                {
                    AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[6], 1f);
                    finishedRound3Voice = true;
                }
                this.enabled = false;
            }
        }
        else
        {
            GetComponent<BuffMenu>().buffMenuActivated = false;
        }


        if (roundStarted && GetComponent<ScenarioManager>().allEnemiesSpawned && FindObjectOfType<SpawnEnemies>().enemiesLeft <= 0)
        {
            if (GetComponent<ScenarioManager>().currentRound == 1 && !GetComponent<BuffMenu>().buffMenuActivated)
            {
                StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
            }
            //if (GetComponent<ScenarioManager>().currentRound == 2 && !GetComponent<BuffMenu>().buffMenuActivated)
            //{
            //    StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
            //}
            if (GetComponent<ScenarioManager>().currentRound == 3 && !GetComponent<BuffMenu>().buffMenuActivated)
            {
                StartCoroutine(GetComponent<BuffMenu>()._BuffMenuActivate());
            }

            // SOLO PARA LA RONDA 0;
            if (!showedOtherWeapons)
            {
                trumpetWeapon.SetActive(true);
                triangleWeapon.SetActive(true);

                AudioManager.instance.PlaySFXOnce(AudioManager.instance.sfxSource, AudioManager.instance.satanVoices[2], 1f);

                showedOtherWeapons = true;
            }
            if (GetComponent<ScenarioManager>().currentRound < 3)
                NextRoundd();
            else
            {
                GetComponent<ScenarioManager>().enemiesThisRound = 0;
                GetComponent<ScenarioManager>().allEnemiesSpawned = false;
                SpawnEnemies_RestartIndex();
                GetComponent<ScenarioManager>().ResetTimer();
                roundStarted = false;
                this.enabled = false;
            }
                

            //GetComponent<ScenarioManager>().GoTimer();
            //if(GetComponent<ScenarioManager>().timeBetweenRoundsCounter <= 0)
            //{
            //    NextRoundd();
            //}
            
        }
    }
    public void NextRoundd()
    {
        GetComponent<ScenarioManager>().currentRound++;
        GetComponent<ScenarioManager>().enemiesThisRound = 0;
        GetComponent<ScenarioManager>().allEnemiesSpawned = false;
        SpawnEnemies_RestartIndex();
        GetComponent<ScenarioManager>().ResetTimer();
        GetComponent<SpawnEnemies>().UpdateRoundCounter();
        //GetComponent<BuffMenu>().counter++;
        //GetComponent<BuffMenu>().FillBar();
        GetComponent<ScenarioManager>().LevelProgressionCheck();
        roundStarted = false;
    }


    bool updatingNavMesh = false;
    public void UpdateNavMesh()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
        updatingNavMesh = true;
    }

    public void SpawnEnemies_Tutorial()
    {
        GameObject enemy1 = Instantiate(enemyPrefabTUTORIAL[0], spawnPositionsTUTORIAL[0].position, spawnPositionsTUTORIAL[0].rotation);
        enemy1.GetComponent<Enemy>().agent = enemy1.GetComponent<NavMeshAgent>();
        enemy1.GetComponent<Enemy>().agent.speed = 0;
        GetComponent<SpawnEnemies>().enemiesLeft++;

        GameObject enemy2 = Instantiate(enemyPrefabTUTORIAL[1], spawnPositionsTUTORIAL[1].position, spawnPositionsTUTORIAL[1].rotation);
        enemy2.GetComponent<Enemy>().agent = enemy2.GetComponent<NavMeshAgent>();
        enemy2.GetComponent<Enemy>().agent.speed = 0;
        GetComponent<SpawnEnemies>().enemiesLeft++;
    }

    public void SpawnEnemies_Tutorial2()
    {
        GameObject enemy1 = Instantiate(enemyPrefabTUTORIAL[0], spawnPositionsTUTORIAL[0].position, Quaternion.identity);
        GetComponent<SpawnEnemies>().enemiesLeft++;

        GameObject enemy2 = Instantiate(enemyPrefabTUTORIAL[1], spawnPositionsTUTORIAL[1].position, Quaternion.identity);
        GetComponent<SpawnEnemies>().enemiesLeft++;

        GameObject enemy3 = Instantiate(enemyPrefabTUTORIAL[2], spawnPositionsTUTORIAL[1].position, Quaternion.identity);
        GetComponent<SpawnEnemies>().enemiesLeft++;
    }

    //Transform GetClosestSpawn(Transform[] spawns)
    //{
    //    Transform nearestSpawn = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;
    //    foreach (Transform t in spawns)
    //    {
    //        float dist = Vector3.Distance(t.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            nearestSpawn = t;
    //            minDist = dist;
    //        }
    //    }
    //    return nearestSpawn;
    //}

    public void SpawnEnemies_Round1()
    {
        if (GetComponent<ScenarioManager>().enemiesOnScreen < GetComponent<ScenarioManager>().maxEnemiesOnScreen)
        {
            int randomNumber = Random.Range(0, 4);
            spawnPositions[randomNumber].gameObject.GetComponent<SpawnTutorial>().Spawn_Round1();
            //Debug.Log(enemiesThisRound);
        }
    }

    public void SpawnEnemies_Round2()
    {
        if (GetComponent<ScenarioManager>().enemiesOnScreen < GetComponent<ScenarioManager>().maxEnemiesOnScreen)
        {
            int randomNumber = Random.Range(0, 4);
            spawnPositions[randomNumber].gameObject.GetComponent<SpawnTutorial>().Spawn_Round2();
            //Debug.Log(enemiesThisRound);
        }
    }

    public void SpawnEnemies_Round3()
    {
        if (GetComponent<ScenarioManager>().enemiesOnScreen < GetComponent<ScenarioManager>().maxEnemiesOnScreen)
        {
            int randomNumber = Random.Range(0, 4);
            spawnPositions[randomNumber].gameObject.GetComponent<SpawnTutorial>().Spawn_Round3();
            //Debug.Log(enemiesThisRound);
        }
    }

    public void SpawnEnemies_RestartIndex()
    {
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i].GetComponent<SpawnTutorial>().ResetIndex();
        }
    }
}

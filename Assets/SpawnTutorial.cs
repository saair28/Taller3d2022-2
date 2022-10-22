using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorial : MonoBehaviour
{
    public GameObject[] enemies_round1;
    public GameObject[] enemies_round2;
    public GameObject[] enemies_round3;

    public int index = 0;
    float timerCount = 0;
    float timer = 1f;

    public void Spawn_Round1()
    {
        if (index < enemies_round1.Length)
        {
            GameObject enemy;
            if(ScenarioManager.instance.enemiesOnScreen >= ScenarioManager.instance.maxEnemiesOnScreen)
            {
                timerCount = timer;
            }
            else
            {
                timerCount -= Time.deltaTime;
            }

            if(timerCount <= 0)
            {
                enemy = Instantiate(enemies_round1[index], transform.position, Quaternion.identity);
                SpawnEnemies.instance.enemiesLeft++;
                ScenarioManager.instance.enemiesThisRound++;
                SpawnEnemies.instance.UpdateEnemyCounter();
                index++;
            }
        }
    }
    public void Spawn_Round2()
    {
        if (index < enemies_round2.Length)
        {
            GameObject enemy;
            if (ScenarioManager.instance.enemiesOnScreen >= ScenarioManager.instance.maxEnemiesOnScreen)
            {
                timerCount = timer;
            }
            else
            {
                timerCount -= Time.deltaTime;
            }

            if (timerCount <= 0)
            {
                enemy = Instantiate(enemies_round2[index], transform.position, Quaternion.identity);
                SpawnEnemies.instance.enemiesLeft++;
                ScenarioManager.instance.enemiesThisRound++;
                SpawnEnemies.instance.UpdateEnemyCounter();
                index++;
            }
            
        }
    }
    public void Spawn_Round3()
    {
        if(index < enemies_round3.Length)
        {
            GameObject enemy;
            if (ScenarioManager.instance.enemiesOnScreen >= ScenarioManager.instance.maxEnemiesOnScreen)
            {
                timerCount = timer;
            }
            else
            {
                timerCount -= Time.deltaTime;
            }

            if (timerCount <= 0)
            {
                enemy = Instantiate(enemies_round3[index], transform.position, Quaternion.identity);
                SpawnEnemies.instance.enemiesLeft++;
                ScenarioManager.instance.enemiesThisRound++;
                SpawnEnemies.instance.UpdateEnemyCounter();
                index++;
            }
        }
    }

    public void ResetIndex()
    {
        index = 0;
    }
}

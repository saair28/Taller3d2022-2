using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffMenu : MonoBehaviour
{
    public bool buffMenuActivated = false;
    public GameObject buffMenuObject;

    GameObject player;

    public GameObject buffBarObject;
    public Image buffBar_fill;

    public int counter = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //FillBar();
    }


    //public void FillBar()
    //{
    //    if((GetComponent<ScenarioManager>().currentRound) % 3 == 1 || GetComponent<ScenarioManager>().currentRound == -1 || GetComponent<ScenarioManager>().currentRound == 0 || GetComponent<ScenarioManager>().currentRound == 0)
    //        buffBar_fill.fillAmount = 0.333f;
    //    else if ((GetComponent<ScenarioManager>().currentRound) % 3 == 2)
    //        buffBar_fill.fillAmount = 0.666f;
    //    else if ((GetComponent<ScenarioManager>().currentRound) % 3 == 0 && GetComponent<ScenarioManager>().currentRound != 0)
    //        buffBar_fill.fillAmount = 0.999f;
    //}

    public void FillBar()
    {
        //GetComponent<BuffMenu>().counter++;
        buffBar_fill.fillAmount = (float)counter / (float)GetComponent<ScenarioManager>().buffMenuRounds;
    }

    public void ResetBar()
    {
        //buffBar_fill.fillAmount = 0.333f;
        buffBar_fill.fillAmount = 0f;
        counter = 0;
    }

    //public void BuffMenuActivate()
    //{
    //    buffMenuObject.SetActive(true);
    //    GetComponent<PauseOptionsMenu>().isPaused = true;
    //    player.GetComponent<PlayerShoot>().enabled = false;
    //    player.GetComponent<PlayerMovement>().enabled = false;
    //    buffMenuActivated = true;
    //    Time.timeScale = 0;
    //}

    public IEnumerator _BuffMenuActivate()
    {
        yield return new WaitForSeconds(1);
        buffMenuObject.SetActive(true);
        ResetBar();
        GetComponent<PauseOptionsMenu>().isPaused = true;
        player.GetComponent<PlayerShoot>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        int temp = GetComponent<ScenarioManager>().buffMenuRounds;
        GetComponent<ScenarioManager>().buffMenuRounds += GetComponent<ScenarioManager>().buffMenuRoundsLast;
        GetComponent<ScenarioManager>().buffMenuRoundsLast = temp;
        buffMenuActivated = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        buffMenuObject.SetActive(false);
        GetComponent<PauseOptionsMenu>().isPaused = false;
        player.GetComponent<PlayerShoot>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        //ResetBar();
    }

    public void BuffHealth()
    {
        player.GetComponent<PlayerHealth>().totalLife += 20;
    }
    public void BuffSpeed()
    {
        player.GetComponent<PlayerMovement>().speedMultiplier += 0.20f;
    }
    public void BuffDamage()
    {
        player.GetComponent<PlayerShoot>().damageMultiplier += 0.12f;
    }
}

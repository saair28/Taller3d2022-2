using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalLife;
    public int actualLife;
    bool hitReceived = false;
    public bool startRecoveryTimer = false;
    public float timeToRecover;
    public float timeToRecoverCount;
    public int lifeRecovery;
    public float invulnerabilityTime;
    public Image lifeHearth;

    void Start()
    {
        actualLife = totalLife;
        timeToRecoverCount = timeToRecover;
    }

    void Update()
    {
        // Lo hice a boleo y funcion�.
        // Lo que hace es algo parecido a COD Zombies, donde si te pegan una vez, comienza un contador para que tu vida se recupere.
        // Una vez recuperas una vida, si te falta otra, el tiempo que tardas en recuperar esta 2da vida es menor.
        if(!startRecoveryTimer)
        {
            timeToRecoverCount = timeToRecover;
        }

        if(startRecoveryTimer)
        {
            if(actualLife < totalLife)
            {
                timeToRecoverCount -= Time.deltaTime;
            }
            else
            {
                startRecoveryTimer = false;
            }
            
            if (timeToRecoverCount <= 0)
            {
                RecoverLife(lifeRecovery);
                timeToRecoverCount = timeToRecover/2;
            }
        }

        //lifeHearth.fillAmount = actualLife / totalLife;
        
    }

    public void RecoverLife(int amount)
    {
        actualLife += amount;
    }

    public IEnumerator GotHit(int damage)
    {
        hitReceived = true;
        startRecoveryTimer = false;
        if (actualLife > 1)
        {
            actualLife -= damage;
        }
        else
        {
            SceneManager.LoadScene(1);
            Debug.Log("MORISTE");
        }
        yield return new WaitForSeconds(invulnerabilityTime);
        Debug.Log("HOLA");
        startRecoveryTimer = true;
        hitReceived = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Danger") && !hitReceived)
        {
            // Aqu�, en lugar de poner un valor como "1", habr�a que usar una variable que indique el da�o del enemigo.
            // Eso se har�a jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
            StartCoroutine(GotHit(25));
        }
        if (col.CompareTag("Enemy1") && !hitReceived)
        {
            // Aqu�, en lugar de poner un valor como "1", habr�a que usar una variable que indique el da�o del enemigo.
            // Eso se har�a jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
            StartCoroutine(GotHit(40));
        }
        if (col.CompareTag("Enemy2") && !hitReceived)
        {
            // Aqu�, en lugar de poner un valor como "1", habr�a que usar una variable que indique el da�o del enemigo.
            // Eso se har�a jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
            StartCoroutine(GotHit(28));
        }

    }
}

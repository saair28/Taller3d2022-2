using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int totalLife;
    public int actualLife;
    bool hitReceived = false;
    public bool startRecoveryTimer = false;
    public float timeToRecover;
    public float timeToRecoverCount;
    public float invulnerabilityTime;

    void Start()
    {
        actualLife = totalLife;
        timeToRecoverCount = timeToRecover;
    }

    void Update()
    {
        // Lo hice a boleo y funcionó.
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
                actualLife++;
                timeToRecoverCount = timeToRecover/2;
            }
        }
        
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
        if(col.CompareTag("Danger") && !hitReceived)
        {
            // Aquí, en lugar de poner un valor como "1", habría que usar una variable que indique el daño del enemigo.
            // Eso se haría jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
            StartCoroutine(GotHit(1));
        }
    }
}

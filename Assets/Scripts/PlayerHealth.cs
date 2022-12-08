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

    public Image pentagram;
    public Image bloodSplatter;
    float splatterAlpha = 0;

    public AudioSource hitSource;

    void Start()
    {
        actualLife = totalLife;
        timeToRecoverCount = timeToRecover;

        if(pentagram != null)
            pentagram.gameObject.SetActive(false);
    }

    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    a += 1f;
        //}
        //a -= 0.001f;
        //a = Mathf.Clamp(a, 0, 1f);
        //ChangeColor();
        // Lo hice a boleo y funcionó.
        // Lo que hace es algo parecido a COD Zombies, donde si te pegan una vez, comienza un contador para que tu vida se recupere.
        // Una vez recuperas una vida, si te falta otra, el tiempo que tardas en recuperar esta 2da vida es menor.
        if (!startRecoveryTimer)
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

        splatterAlpha = (float)(totalLife - actualLife) / totalLife; //Así está normalizado;
        Color c = new Color(bloodSplatter.color.r, bloodSplatter.color.g, bloodSplatter.color.b, splatterAlpha);
        bloodSplatter.color = c;
    }

    public IEnumerator GotHit(int damage)
    {
        hitReceived = true;
        pentagram.gameObject.SetActive(true);

        AudioManager.instance.PlaySFX(hitSource, AudioManager.instance.playerHitSFX, 0.5f);

        startRecoveryTimer = false;
        if (actualLife > 1)
        {
            actualLife -= damage;
            AddSplatter();
        }
        else
        {
            SceneManager.LoadScene("Lose");
            Debug.Log("MORISTE");
        }
        yield return new WaitForSeconds(invulnerabilityTime);
        Debug.Log("HOLA");
        startRecoveryTimer = true;
        hitReceived = false;
        pentagram.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Danger") && !hitReceived)
        {

            // Aquí, en lugar de poner un valor como "1", habría que usar una variable que indique el daño del enemigo.
            // Eso se haría jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
            StartCoroutine(GotHit(25));
            
        }
        //if (col.CompareTag("Enemy1") && !hitReceived)
        //{
        //    // Aquí, en lugar de poner un valor como "1", habría que usar una variable que indique el daño del enemigo.
        //    // Eso se haría jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
        //    StartCoroutine(GotHit(40));
        //}
        //if (col.CompareTag("Enemy2") && !hitReceived)
        //{
        //    // Aquí, en lugar de poner un valor como "1", habría que usar una variable que indique el daño del enemigo.
        //    // Eso se haría jalando la variable "damage" (o como se llame) del script del enemigo que colisiona contra el Player.
        //    StartCoroutine(GotHit(28));
        //}

    }
    
    
    private void AddSplatter()
    {
        splatterAlpha = (float)(totalLife - actualLife)/totalLife; //Así está normalizado;
        Color c = new Color(bloodSplatter.color.r, bloodSplatter.color.g, bloodSplatter.color.b, splatterAlpha);
        bloodSplatter.color = c;
    }
}

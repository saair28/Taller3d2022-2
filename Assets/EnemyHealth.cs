using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float damage;
    public int points;

    public enum EnemyColor
    {
        azul,
        rojo,
        verde
    }
    public EnemyColor enemyColor;
    //public int enemyColor;
    //0 = AZUL
    //1 = ROJO
    //2 = VERDE

    public float normalDamage, criticalDamage;

    public float invulTime;
    public float invulTimeCounter;
    bool isInvulnerable = false;

    TextMeshProUGUI criticoText;

    void Start()
    {
        currentHealth = maxHealth;
        invulTimeCounter = invulTime;

        //criticoText = GameObject.FindGameObjectWithTag("CriticoText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(invulTimeCounter <= 0)
        {
            invulTimeCounter = invulTime;
            isInvulnerable = false;
        }

        if(isInvulnerable)
        {
            invulTimeCounter -= Time.deltaTime;
        }
    }

    public void LoseHealth(float amount)
    {
        if(currentHealth <= 0)
        {
            Death();
        }
        currentHealth -= amount;
        isInvulnerable = true;
        //Debug.Log(amount);
        //criticoText.text = amount.ToString();
    }

    public void Death()
    {
        FindObjectOfType<SpawnEnemies>().enemiesLeft--;
        FindObjectOfType<SpawnEnemies>().UpdateEnemyCounter();
        FindObjectOfType<GameManager>().AddPoints(points);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BalaAZUL"))
        {
            if(!isInvulnerable)
            {
                normalDamage = FindObjectOfType<PlayerShoot>().damage;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.rojo:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.verde:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                }
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BalaROJA"))
        {
            if (!isInvulnerable)
            {
                normalDamage = FindObjectOfType<PlayerShoot>().damage;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.rojo: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.verde:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                }
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BalaVERDE"))
        {
            if (!isInvulnerable)
            {
                normalDamage = FindObjectOfType<PlayerShoot>().damage;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.rojo:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.verde: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                }
            }
            Destroy(other.gameObject);
        }
    }
}

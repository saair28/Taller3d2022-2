using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float damage;

    public int enemyColor;
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

        criticoText = GameObject.FindGameObjectWithTag("CriticoText").GetComponent<TextMeshProUGUI>();
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
        Debug.Log(amount);
        criticoText.text = amount.ToString();
    }

    public void Death()
    {
        FindObjectOfType<SpawnEnemies>().enemiesLeft--;
        FindObjectOfType<SpawnEnemies>().UpdateEnemyCounter();
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
                    case 0: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                    case 1:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case 2:
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
                    case 0:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case 1: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                    case 2:
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
                    case 0:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case 1:
                        LoseHealth(normalDamage);
                        Debug.Log("NORMAL");
                        break;
                    case 2: //ESTE ES CRITICO
                        LoseHealth(criticalDamage);
                        Debug.Log("CRITICO");
                        break;
                }
            }
            Destroy(other.gameObject);
        }
    }
}

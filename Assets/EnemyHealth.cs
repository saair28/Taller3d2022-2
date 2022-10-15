using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float damage;
    public int points;

    public GameObject damageTextPrefab;

    Color _blue = new Color(45, 126, 255, 255);
    Color _red = new Color(255, 64, 40, 255);
    Color _purple = new Color(165, 69, 255, 255);

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

        _blue = new Color(45, 126, 255, 255)/255;
        _red = new Color(255, 64, 40, 255)/255;
        _purple = new Color(165, 69, 255, 255)/255;

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

    public void LoseHealth(float amount, Color _color)
    {
        if(currentHealth <= 0)
        {
            Death();
        }
        currentHealth -= amount;
        isInvulnerable = true;

        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = amount.ToString();
        text.GetComponent<TextMesh>().color = _color;
        //Debug.Log(amount);
        //criticoText.text = amount.ToString();
    }

    public void Death()
    {
        FindObjectOfType<SpawnEnemies>().enemiesLeft--;
        FindObjectOfType<SpawnEnemies>().UpdateEnemyCounter();
        FindObjectOfType<GameManager>().AddPoints(points);
        StartCoroutine(DeathCoroutine());
    }

    public IEnumerator DeathCoroutine()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(0.1f);
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
                        LoseHealth(criticalDamage, _blue);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.rojo:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.verde:
                        LoseHealth(normalDamage, Color.white);
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
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.rojo: //ESTE ES CRITICO
                        LoseHealth(criticalDamage, _red);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.verde:
                        LoseHealth(normalDamage, Color.white);
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
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.rojo:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.verde: //ESTE ES CRITICO
                        LoseHealth(criticalDamage, _purple);
                        Debug.Log("CRITICO");
                        break;
                }
            }
            Destroy(other.gameObject);
        }
    }
}

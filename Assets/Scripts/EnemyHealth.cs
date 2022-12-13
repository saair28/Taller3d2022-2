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

    //public CapsuleCollider boxenemy;

  //  Animator anim;

    public GameObject damageTextPrefab;

    Color _blue = new Color(45, 126, 255, 255);
    Color _red = new Color(255, 64, 40, 255);
    Color _purple = new Color(165, 69, 255, 255);

    public enum EnemyColor
    {
        amarillo,
        azul,
        morado
    }
    public EnemyColor enemyColor;
    public int enemyColorINT;
    //public int enemyColor;
    //0 = amarillo
    //1 = azul
    //2 = morado

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

     //   anim = GetComponent<Animator>();

        //criticoText = GameObject.FindGameObjectWithTag("CriticoText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //CHEAT PARA MATAR A TODOS LOS ENEMIGOS DE LA ZONA
        /*if(Input.GetKeyDown(KeyCode.K))
        {
            Death();
        }
        */
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
        GetComponentInChildren<Animator>().SetTrigger("Damage");
        //GetComponentInChildren<Animator>().SetBool("Damage", false);
        //GetComponentInChildren<Animator>().SetBool("Move", true);
        if (currentHealth - amount <= 0)
        {
            Death();
        }

        currentHealth -= amount;

        isInvulnerable = true;

        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = amount.ToString("F1");
        text.GetComponent<TextMesh>().color = _color;
        //Debug.Log(amount);
        //criticoText.text = amount.ToString();
    }

    public void Death()
    {
        //FindObjectOfType<SpawnEnemies>().enemiesLeft--;
        //FindObjectOfType<SpawnEnemies>().UpdateEnemyCounter();
        FindObjectOfType<GameManager>().AddPoints(points);
        StartCoroutine(DeathCoroutine());
        GetComponentInChildren<Animator>().SetTrigger("Death");
    }

    bool running = false;
    public IEnumerator DeathCoroutine()
    {
        running = true;
        if(running)
        {
            GetComponentInChildren<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            //Destroy(boxenemy);
            yield return new WaitForSeconds(1.9f);
            //Destroy(boxenemy);

            FindObjectOfType<SpawnEnemies>().enemiesLeft--;
            FindObjectOfType<SpawnEnemies>().UpdateEnemyCounter();

            FindObjectOfType<DestroyFarEnemies>().enemyList.Remove(gameObject);

            running = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BalaAZUL"))
        {
            if(!isInvulnerable)
            {
                normalDamage = FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul: //ESTE ES CRITICO
                        LoseHealth(criticalDamage, _blue);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.amarillo:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.morado:
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
                normalDamage = FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.amarillo: //ESTE ES CRITICO
                        LoseHealth(criticalDamage, Color.yellow);
                        Debug.Log("CRITICO");
                        break;
                    case EnemyColor.morado:
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
                normalDamage = FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier;
                criticalDamage = (FindObjectOfType<PlayerShoot>().damage * FindObjectOfType<PlayerShoot>().damageMultiplier * 2.5f);
                switch (enemyColor)
                {
                    case EnemyColor.azul:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.amarillo:
                        LoseHealth(normalDamage, Color.white);
                        Debug.Log("NORMAL");
                        break;
                    case EnemyColor.morado: //ESTE ES CRITICO
                        LoseHealth(criticalDamage, _purple);
                        Debug.Log("CRITICO");
                        break;
                }
            }
            Destroy(other.gameObject);
        }
    }
}

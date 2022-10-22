using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalPoints;
    public TextMeshProUGUI pointsText;

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        pointsText.text = "Puntos: " + totalPoints.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<PlayerHealth>().totalLife = 1000000;
            FindObjectOfType<PlayerHealth>().actualLife = 1000000;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            FindObjectOfType<PlayerShoot>().damageMultiplier = 10;
        }
    }
}

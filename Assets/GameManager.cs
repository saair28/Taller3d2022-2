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
}

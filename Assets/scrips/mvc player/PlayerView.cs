using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour
{
    PlayerModel playerModel;
    PlayerControler playerControl;
    public Text Health_text;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControler>();
        playerModel = GetComponent<PlayerModel>();
    }

    // Update is called once per frame
    public void Update()
    {
        playerControl.Move();
    }
    public void changelife(int value)
    {
        playerModel.vidas += value;
        if (Health_text)
        {
            Health_text.text = "Vidas:  " + playerModel.vidas;
        }
        if (playerModel.vidas <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Derrota");
        }

        if (playerModel.vidas >= playerModel.vidas_max)
        {
            playerModel.vidas = playerModel.vidas_standard;
        }
    }
}

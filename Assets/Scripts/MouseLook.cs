using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;
    float mouseX, mouseY;
    public Transform playerBody;
    float xRotation = 0f;
    public bool TRYNGTHINGS = true; // BORRAR O PONER EN FALSE CUANDO EXPORTEMOS EL FINAL!!!
    public float sens;


    public Slider sensSlider;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sensSlider.value = sens;
    }

    void Update()
    {
        if(TRYNGTHINGS)
        {
            mouseX = Input.GetAxis("Mouse X") * sens * 10 * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sens * 10 * Time.deltaTime;
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        }
        

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void AdjustSensibility(float value)
    {
        sens = value;
    }
}

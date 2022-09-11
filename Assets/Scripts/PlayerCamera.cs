using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private new Transform camera;
    [SerializeField] Vector2 sensibilidad;
    [SerializeField] GameObject player;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        if (hor != 0 && Time.timeScale != 0f)
        {
            player.transform.Rotate(Vector3.up * hor*sensibilidad.x);
            Cursor.visible = false;
        }
        if (ver !=0 && Time.timeScale != 0f)
        {
            float angulo = (camera.localEulerAngles.x - ver * sensibilidad.y + 360) % 360;
            if(angulo>180)
            {
                angulo -= 360;
            }
            angulo = Mathf.Clamp(angulo, -80,80);
            camera.localEulerAngles = Vector3.right * angulo;
            Cursor.visible = false;
        }        
    }
}

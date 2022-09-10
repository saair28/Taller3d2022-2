using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraper : MonoBehaviour
{
    private new Transform camera;
    public Vector2 sensibilidad;
    void Start()
    {
        camera = transform.Find("Camara");
       Cursor.visible = false;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        if (hor != 0 && Time.timeScale != 0f)
        {
            transform.Rotate(Vector3.up * hor*sensibilidad.x);
           Cursor.visible = false;
        }
        if (ver !=0 && Time.timeScale != 0f)
        {
          //  camera.Rotate(Vector3.left * ver *sensibilidad.y);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerdesacti : MonoBehaviour
{
    public GameObject canvas1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (canvas1)
        {
            Destroy(canvas1, 1f);
        }
    }
}

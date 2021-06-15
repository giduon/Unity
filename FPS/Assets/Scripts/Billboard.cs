using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool isUI = false;

    float direction;
   
    void Start()
    {
        direction = (isUI == true) ? 1.0f : -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 눈(카메라)을 향하고 싶다.
        transform.forward = Camera.main.transform.forward * -1.0f;
    }
}

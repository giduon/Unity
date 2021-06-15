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
        // �÷��̾��� ��(ī�޶�)�� ���ϰ� �ʹ�.
        transform.forward = Camera.main.transform.forward * -1.0f;
    }
}

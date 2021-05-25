using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // ���콺�� �����ӿ� ���� ī�޶� ȸ����Ű�� �ʹ�. 
    // �ʿ� ��� : ���콺 �Է�, ȸ�� ����, �ӷ�

    // �ӷ�
    public float rotSpeed = 5.0f;

    float rotX=0;
    float rotY=0;

    public bool rotateX = false;
    public bool rotateY = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 �Է�
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // �� ����
        if (rotateX)
        {
            rotX += mx * rotSpeed * Time.deltaTime;
        }
        if (rotateY)
        {
            rotY += my * rotSpeed * Time.deltaTime;
        }

        // rotY�� ���� -60�� ~ 60�� ���̷� �����Ѵ�.
        rotY = Mathf.Clamp(rotY, -60.0f, 60.0f);


        // ȸ��
        Vector3 dir = new Vector3(-rotY, rotX, 0);
        transform.localEulerAngles = dir;

    }
}

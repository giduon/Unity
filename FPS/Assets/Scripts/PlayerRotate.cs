using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // 마우스의 움직임에 따라 카메라를 회전시키고 싶다. 
    // 필요 요소 : 마우스 입력, 회전 방향, 속력

    // 속력
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
        // 마우스 입력
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // 값 누적
        if (rotateX)
        {
            rotX += mx * rotSpeed * Time.deltaTime;
        }
        if (rotateY)
        {
            rotY += my * rotSpeed * Time.deltaTime;
        }

        // rotY의 값을 -60도 ~ 60도 사이로 제한한다.
        rotY = Mathf.Clamp(rotY, -60.0f, 60.0f);


        // 회전
        Vector3 dir = new Vector3(-rotY, rotX, 0);
        transform.localEulerAngles = dir;

    }
}

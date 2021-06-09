using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어를 이동시키고 싶다. 
    // 필요 요소 : 이동 방향, 이동 속도, 키보드 입력
    
    // 이동 속도
    public float walkSpeed = 3.5f;

    public float runSpeed = 7.0f;
    
    // 플레이어에게 중력을 적용하고 싶다. 
    // 필요 요소 : 중력의 크기, 중력의 방향
    public float gravity = -20.0f;
    float yVelocity = 0;

    // space 키를 누르면 점프를 하고 싶다.
    // 단, 2회까지만 점프를 하고 싶다. 
    // 필요 요소 : 키 입력, 점프력 , 점프 카운트, 

    // 점프력
    public float jumpPower = 5.0f;
    public float jumpCount = 2;
    public int healthPoint = 10;
    float tongtong = 0;
    bool isJump = false;
    CharacterController cc;
    Animator playerAnim;
    float moveSpeed = 0;

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        tongtong = jumpPower;
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키 입력을 받는다. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        

        // 방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // 방향으로 이동한다. P = P0 * vt
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = dir.magnitude * runSpeed;
        }
        else
        {
            moveSpeed = dir.magnitude * walkSpeed;

        }
        // 중력 값을 적용한다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;


        //moveSpeed = Input.GetKeyDown(KeyCode.LeftShift) == true ? dir.magnitude * runSpeed : dir.magnitude * walkSpeed;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 플레이어의 현재 속도를 Animator 의 "PlayerSpeed" 파라미터에 전달한다. 
        playerAnim.SetFloat("PlayerSpeed", moveSpeed / runSpeed);

        // 방향 벡터를 카메라의 방향을 기준으로 재계산한다.
        dir = Camera.main.transform.TransformDirection(dir);

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 2;
            yVelocity = 0;

            // 통통 튕겨오르기
            if (isJump)
            {
                tongtong *= 0.4f;

                if (tongtong > 0.1f)
                {
                    yVelocity = tongtong;
                }
                else
                {
                    tongtong = jumpPower;
                    isJump = false;
                }
            }
        }

        // 사용자의 space 키 입력을 받는다.
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // 위쪽 방향으로의 힘(점프력)을 추가한다.
            //yVelocity = jumpPower;
            yVelocity = tongtong;
            jumpCount--;
            isJump = true;
        }



        
    }

    public void ApplyDamage(int val)
    {
        healthPoint -= val;
        healthPoint = Mathf.Max(healthPoint, 0);
       // print("현재 체럭: " + healthPoint);
    }

}


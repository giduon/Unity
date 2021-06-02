using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �÷��̾ �̵���Ű�� �ʹ�. 
    // �ʿ� ��� : �̵� ����, �̵� �ӵ�, Ű���� �Է�
    
    // �̵� �ӵ�
    public float moveSpeed = 5.0f;
    
    // �÷��̾�� �߷��� �����ϰ� �ʹ�. 
    // �ʿ� ��� : �߷��� ũ��, �߷��� ����
    public float gravity = -20.0f;
    float yVelocity = 0;

    // space Ű�� ������ ������ �ϰ� �ʹ�.
    // ��, 2ȸ������ ������ �ϰ� �ʹ�. 
    // �ʿ� ��� : Ű �Է�, ������ , ���� ī��Ʈ, 

    // ������
    public float jumpPower = 5.0f;
    public float jumpCount = 2;

    public int healthPoint = 10;

    float tongtong = 0;
    bool isJump = false;

    

    CharacterController cc;

    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        tongtong = jumpPower;
    }

    // Update is called once per frame
    void Update()
    {
        // Ű �Է��� �޴´�. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        

        // ������ �����Ѵ�.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // ���� ���͸� ī�޶��� ������ �������� �����Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 2;
            yVelocity = 0;

            // ���� ƨ�ܿ�����
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

        // ������� space Ű �Է��� �޴´�.
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // ���� ���������� ��(������)�� �߰��Ѵ�.
            //yVelocity = jumpPower;
            yVelocity = tongtong;
            jumpCount--;
            isJump = true;
        }

        // �߷� ���� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // �������� �̵��Ѵ�. P = P0 * vt
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    public void ApplyDamage(int val)
    {
        healthPoint -= val;
        healthPoint = Mathf.Max(healthPoint, 0);
        print("���� ü��: " + healthPoint);
    }

}


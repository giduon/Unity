using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    // ������ ���
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        AttackToMove,
        Damaged,
        Die
    }

    public EnemyState eState;

    public float sightRange = 5.0f;
    public float attackRange = 2.0f;
    public float moveSpeed = 9.0f;
    public int attackPower = 2;
    public float delayTime = 1.0f;

    float currentTime = 0;

    public Quaternion startRotation;

    bool isBooked = false;
   int healthPoint = 10;

    Transform player;
    CharacterController cc;
    float rotRate = 0;

    void Start()
    {
        // ������ ���´� ��� �����̴�.
        eState = EnemyState.Idle;

        // �÷��̾ ã�´�.
        player = GameObject.Find("Player").transform;

        cc = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        // �� ���º� ó��
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
                // case EnemyState.Damaged:
                //break;
               
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
            
        }

    }

    void Idle()
    {
        // ��� �ִϸ��̼��� �����Ѵ�.

        // ����, �þ� ������ �÷��̾ ������ �̵� ���·� ��ȯ�Ѵ�.
        // �ʿ� ���: �þ� ����, �÷��̾�� ������ �Ÿ�, �÷��̾�
        float distance = (player.position - transform.position).magnitude;

        if (sightRange >= distance)
        {
            eState = EnemyState.Move;

            // ���� ȸ�� ���¸� startRot�� �����Ѵ�.
            startRotation = transform.rotation;
            // ȸ�� ������ ���� rotRate�� 0���� �ʱ�ȭ�Ѵ�
            rotRate = 0;
        }
    }

    private void Move()
    {
        // �÷��̾� �������� �̵��Ѵ�.
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // �̵� ������ �ٶ󺸵��� ȸ���Ѵ�.
        //transform.rotation = Quaternion.LookRotation(dir);
        
        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime;
        // ���� ������ �̿��Ͽ� ȸ���� �Ѵ�. 
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


        // ����, �÷��̾���� �Ÿ��� ���� ���� �̳��� �����ߴٸ� ���� ���·� ��ȯ�Ѵ�.
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = delayTime;
            CancelInvoke();
        }
    }

    private void Attack()
    {
        // ������ �ð�(����)���� Ÿ���� ü���� ���� ���ݷ¸�ŭ ���ҽ�Ų��. 
        // �ʿ� ��� : �� ���ݷ�, ����, �÷��̾� ü��

        PlayerMove pm = player.GetComponent<PlayerMove>();



        currentTime += Time.deltaTime;
        
        if( Vector3.Distance(player.position,transform.position) < attackRange)
        {
            if (currentTime > delayTime) 
            {
                pm.ApplyDamage(attackPower);
                currentTime = 0;
                print("����");
                
            }
        }

        else
        {
            //if(!isBooked)
            //{
            //// 1.5�� �ڿ� �̵� ���·� ��ȯ�Ѵ�. 
            //    Invoke("SetMoveState", 1.5f);
            //    isBooked = true;

            //}
            Invoke("SetMoveState", 1.5f);
            eState = EnemyState.AttackToMove;

        }
    }

    void SetMoveState()
    {
        // �̵� ���·� ��ȯ�Ѵ�. 
        eState = EnemyState.Move;

        // ���� ȸ�� ���¸� startRot�� �����Ѵ�.
        startRotation = transform.rotation;
        // ȸ�� ������ ���� rotRate�� 0���� �ʱ�ȭ�Ѵ�
        rotRate = 0;
    }

    // �ǰ� ó�� �Լ�
    public void Damaged(int val)
    {
        if(eState != EnemyState.Damaged)
        { 
        healthPoint = Mathf.Max(healthPoint - val, 0);
        eState = EnemyState.Damaged;

        Invoke("ReturnState", 0.05f);
        }
    }
    void ReturnState()
    {
        eState = EnemyState.Move;
    }

    private void Die()
    {

    }

}
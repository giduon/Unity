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

    Transform player;
    CharacterController cc;
    float rotRate = 0;
    Quaternion startRotation;
    float currentTime = 0;
    bool isBooked = false;
    public int healthPoint = 100;
    Animator enemyAnim;

    public Transform GetTargetTrasform()
    {
        return player;
    }



    void Start()
    {
        // ������ ���´� ��� �����̴�.
        eState = EnemyState.Idle;

        // �÷��̾ ã�´�.
        player = GameObject.Find("Player").transform;

        cc = transform.GetComponent<CharacterController>();

        // �ڽ� ������Ʈ�κ��� Animator ������Ʈ�� �����´�
        enemyAnim = GetComponentInChildren<Animator>();
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
            case EnemyState.Damaged:
                CheckClipTime();
                break;
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
            SetMoveState();
            //eState = EnemyState.Move;

            //// ���� ȸ�� ���¸� startRot�� �����Ѵ�.
            //startRotation = transform.rotation;
            //// ȸ�� ������ ���� rotRate�� 0���� �ʱ�ȭ�Ѵ�
            //rotRate = 0;
        }
    }

    private void Move()
    {
        // �÷��̾� �������� �̵��Ѵ�.
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        
        // ����, �÷��̾���� �Ÿ��� ���� ���� �̳��� �����ߴٸ� ���� ���·� ��ȯ�Ѵ�.
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = 0;
            //CancelInvoke();


            // ���� �ִϸ��̼��� �����Ѵ�
            enemyAnim.SetTrigger("MoveToAttack");

            return;
        }

        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // �̵� ������ �ٶ󺸵��� ȸ���Ѵ�.
        //transform.rotation = Quaternion.LookRotation(dir);

        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime * 2f;
        // ���� ������ �̿��Ͽ� ȸ���� �Ѵ�. 
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


    }

    private void Attack() // �ִϸ��̼ǰ� �������� ���߱� ���ؼ� ���� �и� �ߴ�. ���⼱ �ִϸ��̼Ǹ� ����.
    {
        // ������ �ð�(����)���� Ÿ���� ü���� ���� ���ݷ¸�ŭ ���ҽ�Ų��. 
        // �ʿ� ��� : �� ���ݷ�, ����, �÷��̾� ü��

       



        currentTime += Time.deltaTime;

        if (Vector3.Distance(player.position, transform.position) < attackRange)
        {
            if (currentTime > delayTime)
            {
                currentTime = 0;
                //print("����");
                enemyAnim.SetTrigger("DelayToAttack");
            }
        }

        else
        {
            if (!isBooked)
            {
                // 1.5�� �ڿ� �̵� ���·� ��ȯ�Ѵ�. 
                Invoke("SetMoveState", 1.5f);
                isBooked = true;

            }
            //Invoke("SetMoveState", 1.5f);
            eState = EnemyState.AttackToMove;

        }
    }

    void SetMoveState()
    {
        // �̵� ���·� ��ȯ�Ѵ�. 
        eState = EnemyState.Move;

        // �̵� �ִϸ��̼��� �����Ѵ�
        enemyAnim.SetTrigger("IdleToMove");
 
        // ���� ȸ�� ���¸� startRot�� �����Ѵ�.
        startRotation = transform.rotation;
        // ȸ�� ������ ���� rotRate�� 0���� �ʱ�ȭ�Ѵ�
        rotRate = 0;

        isBooked = false;
    }

    // �ǰ� ó�� �Լ�
    public void Damaged(int val)
    {
        if (eState != EnemyState.Damaged)
        {
            healthPoint = Mathf.Max(healthPoint - val, 0);
            eState = EnemyState.Damaged;

            // �ǰ� �ִϸ��̼��� ȣ���Ѵ�.
            enemyAnim.SetTrigger("OnHit");

           

            //Invoke("ReturnState", 0.9f);
        }
    }
    private void CheckClipTime()
    {
        // �ǰ� �ִϸ��̼��� �� ���̸� ���Ѵ� 
        AnimatorStateInfo myStateInfo = enemyAnim.GetCurrentAnimatorStateInfo(0);
        //print("length: " + myStateInfo.length);


        // ����, ���� ������ �̸��� "Move State" ���
        if ( myStateInfo.IsName("Move State"))
        {
            ReturnState();
            
            //print("length: " + myStateInfo.length);
            
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
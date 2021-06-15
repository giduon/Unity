using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public int maxHp = 100;

    Transform player;
    CharacterController cc;
    float rotRate = 0;
    Quaternion startRotation;
    float currentTime = 0;
    bool isBooked = false;
    int healthPoint = 0;
    Animator enemyAnim;
    NavMeshAgent smith;

    bool playMoveAni = false;


    public Transform GetTargetTrasform()
    {
        return player;
    }

    public int GetHp()
    {
        return healthPoint;
    }



    void Start()
    {
        healthPoint = maxHp;

        // ������ ���´� ��� �����̴�.
        eState = EnemyState.Idle;

        // �÷��̾ ã�´�.
        player = GameObject.Find("Player").transform;

        cc = transform.GetComponent<CharacterController>();

        // �ڽ� ������Ʈ�κ��� Animator ������Ʈ�� �����´�
        enemyAnim = GetComponentInChildren<Animator>();

        // �Ķ���� �ʱ�ȭ 
        enemyAnim.SetBool("IsDie", false);

        // NavMeshAgent ������Ʈ�� �����´�
        smith = GetComponent<NavMeshAgent>();
        smith.speed = 5.0f;
        smith.acceleration = 5.0f;
        smith.stoppingDistance = attackRange;
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
                //Move();
                Move2();
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

    void Move2()
    {
        smith.enabled = true;

        //�÷��̾��� ��ġ�� �׺�޽��� �������� �����Ѵ�. 
        smith.SetDestination(player.position);

        float dist = Vector3.Distance(player.position, transform.position);
        if(dist <= attackRange)
        {
            eState = EnemyState.Attack;
            enemyAnim.SetTrigger("MoveToAttack");
            smith.isStopped = false;
        }
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
        if(eState == EnemyState.Move)
        {
            playMoveAni = true;

        }
        else
        {
            playMoveAni = false;
        }

        // �׺���̼� �޽��� �����. 
        //smith.isStopped = true;
        smith.enabled = false;

        healthPoint = Mathf.Max(healthPoint - val, 0);

        //���� ü���� 0 �����̸� Die ���·� �����Ѵ�
        if(healthPoint <= 0)
        {
            eState = EnemyState.Die;

            // ���� �ִϸ޴ϼ��� ȣ���Ѵ�.
            enemyAnim.SetBool("IsDie", true);

            // ĳ���� ��Ʈ�ѷ��� ĸ�� �ݶ��̴��� ���� ��Ȱ��ȭ�Ѵ�. 
            cc.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        // �׷��� �ƴϸ�, Damaged ���·� �����Ѵ�. 
        else
        {
            eState = EnemyState.Damaged;

            // �ǰ� �ִϸ��̼��� ȣ���Ѵ�.
            enemyAnim.SetTrigger("OnHit");


        }
           

            //Invoke("ReturnState", 0.9f);
        
    }
    private void CheckClipTime()
    {
        // �ǰ� �ִϸ��̼��� �� ���̸� ���Ѵ� 
        AnimatorStateInfo myStateInfo = enemyAnim.GetCurrentAnimatorStateInfo(0);
        //print("length: " + myStateInfo.length);


        // ����, ���� ������ �̸��� "Move State" ���
        if(playMoveAni)
        {
            if(myStateInfo.IsName("Hit State"))
            {
                playMoveAni = false;
            }
        }
        else
        {

            if  ( myStateInfo.IsName("Move State"))
            {
                ReturnState();
            
            
                //print("length: " + myStateInfo.length);
            
            }
        }

    }

    void ReturnState()
    {
        
        eState = EnemyState.Move;
    }

    private void Die()
    {
        // ����, IsDie �Ķ������ ���� true��� false�� �������ش�. 
        if(enemyAnim.GetBool("IsDie"))
        {
        }
        // ���� , Die �ִϸ��̼��� ���� ���̰�, �ִϸ��̼� �������  1.0(100%)�� �������� ��..
        AnimatorStateInfo mySatte = enemyAnim.GetCurrentAnimatorStateInfo(0);

        if(mySatte.IsName("Die State") )
        {
            enemyAnim.SetBool("IsDie", false);
            if(mySatte.normalizedTime >= 1.0f)
            {
                //�ڱ� �ڽ��� �����Ѵ�.
                Destroy(gameObject);

            }
        }

    }

}
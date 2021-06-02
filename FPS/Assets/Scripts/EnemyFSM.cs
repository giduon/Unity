using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
   
    // 열거형 상수
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
        // 최초의 상태는 대기 상태이다.
        eState = EnemyState.Idle;

        // 플레이어를 찾는다.
        player = GameObject.Find("Player").transform;

        cc = transform.GetComponent<CharacterController>();

        // 자식 오브젝트로부터 Animator 컴포넌트를 가져온다
        enemyAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 각 상태별 처리
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
        // 대기 애니메이션을 실행한다.

        // 만일, 시야 범위에 플레이어가 있으면 이동 상태로 전환한다.
        // 필요 요소: 시야 범위, 플레이어와 나와의 거리, 플레이어
        float distance = (player.position - transform.position).magnitude;

        if (sightRange >= distance)
        {
            SetMoveState();
            //eState = EnemyState.Move;

            //// 현재 회전 상태를 startRot로 저장한다.
            //startRotation = transform.rotation;
            //// 회전 보간을 위한 rotRate도 0으로 초기화한다
            //rotRate = 0;
        }
    }

    private void Move()
    {
        // 플레이어 방향으로 이동한다.
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        
        // 만일, 플레이어와의 거리가 공격 범위 이내로 접근했다면 공격 상태로 전환한다.
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = 0;
            //CancelInvoke();


            // 공격 애니메이션을 실행한다
            enemyAnim.SetTrigger("MoveToAttack");

            return;
        }

        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 이동 방향을 바라보도록 회전한다.
        //transform.rotation = Quaternion.LookRotation(dir);

        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime * 2f;
        // 선형 보간을 이용하여 회전을 한다. 
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


    }

    private void Attack() // 애니메이션과 데미지를 맞추기 위해서 따로 분리 했다. 여기선 애니메이션만 실행.
    {
        // 딜레이 시간(공속)마다 타겟의 체력을 나의 공격력만큼 감소시킨다. 
        // 필요 요소 : 내 공격력, 공속, 플레이어 체력

       



        currentTime += Time.deltaTime;

        if (Vector3.Distance(player.position, transform.position) < attackRange)
        {
            if (currentTime > delayTime)
            {
                currentTime = 0;
                //print("공격");
                enemyAnim.SetTrigger("DelayToAttack");
            }
        }

        else
        {
            if (!isBooked)
            {
                // 1.5초 뒤에 이동 상태로 전환한다. 
                Invoke("SetMoveState", 1.5f);
                isBooked = true;

            }
            //Invoke("SetMoveState", 1.5f);
            eState = EnemyState.AttackToMove;

        }
    }

    void SetMoveState()
    {
        // 이동 상태로 전환한다. 
        eState = EnemyState.Move;

        // 이동 애니메이션을 실행한다
        enemyAnim.SetTrigger("IdleToMove");
 
        // 현재 회전 상태를 startRot로 저장한다.
        startRotation = transform.rotation;
        // 회전 보간을 위한 rotRate도 0으로 초기화한다
        rotRate = 0;

        isBooked = false;
    }

    // 피격 처리 함수
    public void Damaged(int val)
    {
        if (eState != EnemyState.Damaged)
        {
            healthPoint = Mathf.Max(healthPoint - val, 0);
            eState = EnemyState.Damaged;

            // 피격 애니메이션을 호출한다.
            enemyAnim.SetTrigger("OnHit");

           

            //Invoke("ReturnState", 0.9f);
        }
    }
    private void CheckClipTime()
    {
        // 피격 애니메이션의 총 길이를 구한다 
        AnimatorStateInfo myStateInfo = enemyAnim.GetCurrentAnimatorStateInfo(0);
        //print("length: " + myStateInfo.length);


        // 만일, 현재 상태의 이름이 "Move State" 라면
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
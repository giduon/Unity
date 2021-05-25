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
        Damaged,
        Die
    }

    public EnemyState eState;

    public float sightRange = 5.0f;
    public float attackRange = 2.0f;
    public float moveSpeed = 9.0f;

    Transform player;
    CharacterController cc;

    void Start()
    {
        // 최초의 상태는 대기 상태이다.
        eState = EnemyState.Idle;

        // 플레이어를 찾는다.
        player = GameObject.Find("Player").transform;

        cc = transform.GetComponent<CharacterController>();
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
                Damaged();
                break;
            case EnemyState.Die:
                Die();
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
            eState = EnemyState.Move;
        }
    }

    private void Move()
    {
        // 플레이어 방향으로 이동한다.
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 이동 방향을 바라보도록 회전한다.
        transform.rotation = Quaternion.LookRotation(dir);

        // 만일, 플레이어와의 거리가 공격 범위 이내로 접근했다면 공격 상태로 전환한다.
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
        }
    }

    private void Attack()
    {

    }

    private void Damaged()
    {

    }

    private void Die()
    {

    }

}
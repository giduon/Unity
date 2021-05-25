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
        // ��� �ִϸ��̼��� �����Ѵ�.

        // ����, �þ� ������ �÷��̾ ������ �̵� ���·� ��ȯ�Ѵ�.
        // �ʿ� ���: �þ� ����, �÷��̾�� ������ �Ÿ�, �÷��̾�
        float distance = (player.position - transform.position).magnitude;

        if (sightRange >= distance)
        {
            eState = EnemyState.Move;
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
        transform.rotation = Quaternion.LookRotation(dir);

        // ����, �÷��̾���� �Ÿ��� ���� ���� �̳��� �����ߴٸ� ���� ���·� ��ȯ�Ѵ�.
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
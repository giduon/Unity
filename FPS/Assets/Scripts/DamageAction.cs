using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : MonoBehaviour
{
    EnemyFSM efsm;
    void Start()
    {
        efsm = GetComponentInParent<EnemyFSM>();
    }
        // ������ ���� ���ݷ��� ���� ���� �������� ���濡�� ������ �ʹ�. 
    public void OnEnmeyAttack(float damegeRate)
    {
        //EnemyFSM Ŭ�������� �÷��̾ �����´�.
        Transform enemyTarget = efsm.GetTargetTrasform();

        PlayerMove pm = enemyTarget.GetComponent<PlayerMove>();
        int finalDamage = (int)(efsm.attackPower * damegeRate);


        pm.ApplyDamage(finalDamage);
        print("�÷��̾�� " + finalDamage.ToString() + "�� ���ظ� �Ծ����ϴ�!");
    }

}

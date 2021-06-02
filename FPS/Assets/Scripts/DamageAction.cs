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
        // 비율에 따라 공격력의 일정 비율 데미지를 상대방에게 입히고 싶다. 
    public void OnEnmeyAttack(float damegeRate)
    {
        //EnemyFSM 클래스에서 플레이어를 가져온다.
        Transform enemyTarget = efsm.GetTargetTrasform();

        PlayerMove pm = enemyTarget.GetComponent<PlayerMove>();
        int finalDamage = (int)(efsm.attackPower * damegeRate);


        pm.ApplyDamage(finalDamage);
        print("플레이어는 " + finalDamage.ToString() + "의 피해를 입었습니다!");
    }

}

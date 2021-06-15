using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // 1. 대상의 체력 정보를 읽어와서
    // 2. 체력 슬라이더의 값에 반영한다. 
    // 필요 요소 : 대상, 체력 슬라이더

    public EnemyFSM enemy;
    public Image hpslider;

 

    void Update()
    {
        hpslider.fillAmount = (float)enemy.GetHp() / (float)enemy.maxHp;
    }
}

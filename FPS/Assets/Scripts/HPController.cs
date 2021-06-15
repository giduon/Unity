using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // 1. ����� ü�� ������ �о�ͼ�
    // 2. ü�� �����̴��� ���� �ݿ��Ѵ�. 
    // �ʿ� ��� : ���, ü�� �����̴�

    public EnemyFSM enemy;
    public Image hpslider;

 

    void Update()
    {
        hpslider.fillAmount = (float)enemy.GetHp() / (float)enemy.maxHp;
    }
}

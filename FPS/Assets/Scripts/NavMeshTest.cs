using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    NavMeshAgent smith;
    Transform target;

    void Start()
    {
        // �������� �����Ѵ�. 
        target = GameObject.Find("Player").transform;

        // NavMEshAgent ������Ʈ�� �����´�. 
        smith = GetComponent<NavMeshAgent>();

        // NavMeshAgent�� ������ �ʱ�ȭ�Ѵ�. 
       

    }

    // Update is called once per frame
    void Update()
    {
        // ������Ʈ���� �������� �����Ѵ�. 
        smith.SetDestination(target.position);
        
    }
}

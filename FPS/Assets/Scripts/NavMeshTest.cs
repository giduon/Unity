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
        // 목적지를 설정한다. 
        target = GameObject.Find("Player").transform;

        // NavMEshAgent 컴포넌트를 가져온다. 
        smith = GetComponent<NavMeshAgent>();

        // NavMeshAgent의 설정을 초기화한다. 
       

    }

    // Update is called once per frame
    void Update()
    {
        // 에이전트에게 목적지를 전달한다. 
        smith.SetDestination(target.position);
        
    }
}

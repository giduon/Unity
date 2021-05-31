using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    // 앞으로 총알이 나아간다.
    public float power = 20.0f;

    public GameObject eff;
    

    void Start()
    {

    }

    void Update()
    {
        //transform.position += new Vector3(0, 0, 1) * power * Time.deltaTime;
        //transform.position += transform.forward * power * Time.deltaTime;
        Vector3 dir = new Vector3(0, 0, 1);
        dir = transform.TransformDirection(dir);
        transform.position += dir * power * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 대상의 충돌 지점에 피격 이펙트를 생성한다
        ContactPoint[] cPoint = collision.contacts;
        GameObject go = Instantiate(eff);
        go.transform.position = cPoint[0].point;

        // 피격 이페긑의 파티클 방출 방향이 충돌 지점의 노멀 방향과 일치하도록 한다. 
        go.transform.up = cPoint[0].normal;

        // 파티클을 실행한다
        ParticleSystem ps = go.GetComponent<ParticleSystem>();

        ps.Stop();
        ps.Play();

        // 사라지는 이펙트 
        Destroy(gameObject);
    }
}

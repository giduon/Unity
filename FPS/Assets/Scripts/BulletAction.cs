using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    // ������ �Ѿ��� ���ư���.
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
        // �浹�� ����� �浹 ������ �ǰ� ����Ʈ�� �����Ѵ�
        ContactPoint[] cPoint = collision.contacts;
        GameObject go = Instantiate(eff);
        go.transform.position = cPoint[0].point;

        // �ǰ� ����P�� ��ƼŬ ���� ������ �浹 ������ ��� ����� ��ġ�ϵ��� �Ѵ�. 
        go.transform.up = cPoint[0].normal;

        // ��ƼŬ�� �����Ѵ�
        ParticleSystem ps = go.GetComponent<ParticleSystem>();

        ps.Stop();
        ps.Play();

        // ������� ����Ʈ 
        Destroy(gameObject);
    }
}

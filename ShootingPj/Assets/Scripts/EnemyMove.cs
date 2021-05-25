using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 6;
    public GameObject player = null;
    public int rate = 60;
    public GameObject explosionFx;

    

    Vector3 dir;


    void Start()
    {
        

        // ���� �ִ� �÷��̾� ������Ʈ�� ã�´�.
        player = GameObject.Find("Player");

        // rate�� Ȯ���� �÷��̾� �������� �����Ѵ�.
        int draw = Random.Range(1, 101);
        //print(draw);

        if (draw <= rate)
        {
            // ���� �÷��̾ ã�� ���¶��
            if (player != null)
            { 
            dir = player.transform.position - transform.position;
            dir.Normalize();
        }
        }
        // �������� Ȯ���� ������ �Ʒ��� �����Ѵ�.
        else
        {
            dir = Vector3.down;
        }
        
    }

    void Update()
    {
        // ���� �Ʒ��� �������� �ʹ�.
        //transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;

        // �÷��̾ ���ؼ� ���� �ʹ�.
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    // ���� �ε��� ����� �����ϰ�, ���� �����Ѵ�.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // �ε��� ����� �̸��� "Player"��� �۾��� �����ϰ� �ִٸ�...
    //    //if (collision.gameObject.name == "Player")
    //    if (collision.gameObject.name.Contains("Player"))
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        // �ε��� ����� �̸��� "Player"��� �۾��� �����ϰ� �ִٸ�...
        //if (collision.gameObject.name == "Player")
        if (collision.gameObject.name.Contains("Player"))
        {
            GameObject go = Instantiate(explosionFx);
            go.transform.position = collision.transform.position;

            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            ps.Play();

            Destroy(collision.gameObject);
            
            // ��� ������ ��ü�ϰ�, �޴� UI�� ���� 
            SoundManager.sm.PlayGameoverSound();
            UIManager.um.ActivateMenuUI();

            // �ְ� ������ ���Ϸ� �����Ѵ�.
            string result = GameManager.instance.SaveScore();
            print(result);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
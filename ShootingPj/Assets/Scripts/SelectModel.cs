using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModel : MonoBehaviour
{
    // �ʿ��� : �𵨸� �迭

    //public GameObject model1;
    //public GameObject model2;
    //public GameObject model3;
    //public GameObject model4;

    public GameObject[] Models;
   

    void Start()
    {
        // 4���� �𵨸� �߿��� 1������ �����ϰ� �����Ѵ�.
       GameObject selection;
        int ran = Random.Range(0, 4);
        selection = Models[ran];

        //int draw = Random.Range(0, 4);

        //if(draw == 0)
        //{
        //    selection = model1;
        //}
        //else if(draw == 1)
        //{
        //    selection = model2;
        //}
        //else if (draw == 2)
        //{
        //    selection = model3;
        //}
        //else 
        //{
        //    selection = model4;
        //}

        //�� ���õ� �𵨸� �������� ���� ����Ѵ�.
        GameObject go = Instantiate(selection);

        //������ ������ ������Ʈ�� ���� �ڽ� ������Ʈ�� ����Ѵ�.
        go.transform.parent = transform;
        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        go.transform.eulerAngles = new Vector3(90, 180, 0);       
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

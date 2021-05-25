using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModel : MonoBehaviour
{
    // 필요요소 : 모델링 배열

    //public GameObject model1;
    //public GameObject model2;
    //public GameObject model3;
    //public GameObject model4;

    public GameObject[] Models;
   

    void Start()
    {
        // 4개의 모델링 중에서 1가지를 랜덤하게 선택한다.
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

        //그 선택된 모델링 프리팹을 씬에 등록한다.
        GameObject go = Instantiate(selection);

        //생성된 프리팹 오브젝트를 나의 자식 오브젝트로 등록한다.
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

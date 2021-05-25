using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    public float scrollSpeed = 0.1f;
    Material matBG;
    void Start()
    {
        // ��� ������Ʈ���� MeshRenderer ������Ʈ�� �����´�.
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        // MesgRenerer ������Ʈ���� Material�� �����´�. 
        matBG = mr.materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Material�� offset ���� �����ؾ��Ѵ�.
        // offset�� y���� �� �����Ӹ��� ���� ��(����) ��ŭ ������Ų��.
        matBG.mainTextureOffset += new Vector2(0, 1)* scrollSpeed * Time.deltaTime;
    }
}
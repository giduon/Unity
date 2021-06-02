using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetting : MonoBehaviour
{
    // 앱이 시작되면 마우스 커서를 화면 안쪽에서 벗어나지 못하게 하고 싶다. 

    public CursorLockMode myLockMode = CursorLockMode.None;

    void Start()
    {

        Cursor.lockState = myLockMode;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // singleton pattern 
    public static UIManager um;

    public GameObject menuButtons;
    
   

    private void Awake()
    {
        if(um==null)
        {
            um = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    // 앱을 종료한다.
    public void QuitGame()
    {
        print("꾹");
        Application.Quit();
    }

    // 게임 오버 메뉴 UI를 활성화 한다 
    public void ActivateMenuUI()
    {
        menuButtons.SetActive(true);

    }

}

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

    // ���� �����Ѵ�.
    public void QuitGame()
    {
        print("��");
        Application.Quit();
    }

    // ���� ���� �޴� UI�� Ȱ��ȭ �Ѵ� 
    public void ActivateMenuUI()
    {
        menuButtons.SetActive(true);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text curScoreText;
    public Text bestScoreText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int currentScore = 0;
     int bestScore = 0;

    // �÷��̾ ���� ������ ������ 1���� ������ ȹ���Ѵ�. 

    void Start()
    {
        currentScore = 0;
        curScoreText.text = "���� ���ھ� : " + currentScore;
        

        int bestPoint = PlayerPrefs.GetInt("myBestScore");
        bestScore = bestPoint;
        bestScoreText.text = "�ְ� ���ھ� : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���� �߰� �Լ�
    public void AddScore(int point)
    {
        //���� ������ �����Ѵ�. 

        currentScore += point;
        curScoreText.text = "���� ���ھ� : " + currentScore;

        
        bestScoreText.text = "�ְ� ���ھ� : " + bestScore;

        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = "�ְ� ���ھ� : " + bestScore;
        }
    }

    // �ְ� ���� ���� ���
    public string SaveScore()
    {
        PlayerPrefs.SetInt("myBestScore", bestScore);
        return "������ �Ǿ����ϴ�!";
    }
}

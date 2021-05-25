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

    // 플레이어가 적을 격추할 떄마다 1점씩 점수를 획득한다. 

    void Start()
    {
        currentScore = 0;
        curScoreText.text = "현재 스코어 : " + currentScore;
        

        int bestPoint = PlayerPrefs.GetInt("myBestScore");
        bestScore = bestPoint;
        bestScoreText.text = "최고 스코어 : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //점수 추가 함수
    public void AddScore(int point)
    {
        //현재 점수를 가산한다. 

        currentScore += point;
        curScoreText.text = "현재 스코어 : " + currentScore;

        
        bestScoreText.text = "최고 스코어 : " + bestScore;

        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreText.text = "최고 스코어 : " + bestScore;
        }
    }

    // 최고 점수 저장 기능
    public string SaveScore()
    {
        PlayerPrefs.SetInt("myBestScore", bestScore);
        return "저장이 되었습니다!";
    }
}

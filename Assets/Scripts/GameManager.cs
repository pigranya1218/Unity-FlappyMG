using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GAME_STATE
    {
        READY,
        PLAY,
        FINISH,
    }

    public static GameManager instance;

    private GAME_STATE m_state;
    private int m_score;

    public GameObject titleCanvas;
    public GameObject ingameCanvas; 
    public GameObject finishCanvas;

    public ObstacleManager obstacleManager;
    public MGController mgController;

    public Text scoreText;
    public Text currScoreText;
    public Text bestScoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_state = GAME_STATE.READY;
        titleCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(false);
    }

    void Update()
    {
        float time = Time.deltaTime;

        switch (m_state)
        {
            case GAME_STATE.READY:
                break;

            case GAME_STATE.PLAY:
                obstacleManager.update(time);
                break;
            
            case GAME_STATE.FINISH:
                break;
        }
    }

    // 타이틀에서 게임을 시작하는 버튼
    public void ClickStartBtn()
    {
        if (m_state != GAME_STATE.READY) return;

        titleCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        finishCanvas.SetActive(false);

        m_state = GAME_STATE.PLAY;
        m_score = 0;
        scoreText.text = "0";

        mgController.StartGame();
        obstacleManager.StartGame();
    }

    public bool IsGamePlaying()
    {
        return (m_state == GAME_STATE.PLAY);
    }

    public bool IsGameFinish()
    {
        return (m_state == GAME_STATE.FINISH);
    }

    // 게임 오버 함수
    public void SetGameFinish()
    {
        m_state = GAME_STATE.FINISH;

        finishCanvas.SetActive(true);
        ingameCanvas.SetActive(false);

        int bestScore = PlayerPrefs.GetInt("bestScore", 0);

        currScoreText.text = m_score.ToString();

        if (m_score > bestScore)
        {
            bestScore = m_score;
            PlayerPrefs.SetInt("bestScore", bestScore);
            currScoreText.color = Color.yellow;
            bestScoreText.color = Color.yellow;
        }
        else
        {
            currScoreText.color = Color.white;
            bestScoreText.color = Color.white;
        }

        bestScoreText.text = bestScore.ToString();
    }

    // 점수를 더하고 해당 transform 위치에 점수 띄어줌
    public void AddScore()
    {
        m_score += 1;
        scoreText.text = m_score.ToString();
    }

    // 게임이 끝나고 점수 확인한 뒤 다시 타이틀로 돌아가는 버튼
    public void ClickFinishBtn()
    {
        m_state = GAME_STATE.READY;

        titleCanvas.SetActive(true);
        finishCanvas.SetActive(false);

        mgController.ResetGame();
        obstacleManager.ResetGame();
    }
}

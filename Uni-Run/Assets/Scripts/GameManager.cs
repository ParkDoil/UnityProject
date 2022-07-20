using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int ScoreIncreaseAmount = 1;

    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public UnityEvent OnGameEnd = new UnityEvent();

    public int CurrentScore
    {
        get
        {
            return _currnetScore;
        }
        set
        {
            _currnetScore = value;
            // 프로퍼티를 만들때 value키워드를 사용 가능
            // SetScore(int value)랑 동일
            OnScoreChanged.Invoke(_currnetScore);
        }
    }


    private int _currnetScore = 0;
    private bool _isEnd = false;

    void Update()
    {
        if(_isEnd && Input.GetKeyDown(KeyCode.R))
        {
            reset();
            SceneManager.LoadScene(0);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore()
    {
        CurrentScore += ScoreIncreaseAmount;
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void End()
    {
        _isEnd = true;
        OnGameEnd.Invoke();
    }

    void reset()
    {
        _currnetScore = 0;
        _isEnd = false;
    }
}
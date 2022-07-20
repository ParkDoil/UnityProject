using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    private int _obstacleCount;
    private GameObject[] _obstacles; // 장애물 오브젝트들
    private bool _isStepped = false; // 플레이어 캐릭터가 밟았었는가

    void Awake()
    {
        _obstacleCount = transform.childCount;
        _obstacles = new GameObject[_obstacleCount];

        for (int i = 0; i < _obstacleCount; ++i)
        {
            _obstacles[i] = transform.GetChild(i).gameObject;
        }
    }
    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {
        _isStepped = false;

        for (int i = 0; i < _obstacleCount; ++i)
        {
            if (0 == Random.Range(0, 4))
            {
                _obstacles[i].SetActive(true);
            }
            else
            {
                _obstacles[i].SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_isStepped == false)
            {
                _isStepped = true;
                GameManager.Instance.AddScore();
            }
        }
    }
}
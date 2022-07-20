using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab; // 생성할 발판의 원본 프리팹
    public int MaxPlatFormCount = 3; // 생성할 발판의 개수

    public float MinSpawnCooltime = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float MaxSpawnCooltime = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float _nextSpwanCooltime; // 다음 배치까지의 시간 간격
    private float _cooltime; // 마지막 배치 시점

    public float MinY = -3.5f; // 배치할 위치의 최소 y값
    public float MaxY = 1.5f; // 배치할 위치의 최대 y값
    private float _xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] _platforms; // 미리 생성한 발판들
    private int _nextSpawnPlatformIndex = 0; // 사용할 현재 순번의 발판

    private readonly Vector2 _poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치


    void Start()
    {
        _platforms = new GameObject[MaxPlatFormCount];
        for (int i = 0; i < MaxPlatFormCount; ++i)
        {
            _platforms[i] = Instantiate(PlatformPrefab, _poolPosition, Quaternion.identity);
            _platforms[i].SetActive(false);
        }
    }

    void Update()
    {
        _cooltime += Time.deltaTime;

        if (_cooltime >= _nextSpwanCooltime)
        {
            _cooltime = 0f;

            _nextSpwanCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);

            Vector2 spawnPostion = new Vector2(_xPos, Random.Range(MinY, MaxY));
            GameObject currentPlatform = _platforms[_nextSpawnPlatformIndex];
            currentPlatform.transform.position = spawnPostion;
            currentPlatform.SetActive(false);
            currentPlatform.SetActive(true);

            _nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatFormCount;
        }
    }
}
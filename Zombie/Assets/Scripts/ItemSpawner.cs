using UnityEngine;
using UnityEngine.AI; // 내비메쉬 관련 코드

// 주기적으로 아이템을 플레이어 근처에 생성하는 스크립트
public class ItemSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public Transform PlayerTransform;
    public float MaxDistance = 5f;
    public float SpawnCooltime = 1.5f;

    private float _lastSpawnTime = 0f;
    
    // 주기적으로 아이템 생성 처리 실행
    private void Update() 
    {
        if (Time.time >= _lastSpawnTime + SpawnCooltime)
        {
            _lastSpawnTime = Time.time;

            Spawn();
        }
    }

    // 실제 아이템 생성 처리
    private void Spawn()
    {    
        Vector3 spawnPosition =
            GetRandomPointOnNavMesh(PlayerTransform.position, MaxDistance);
        spawnPosition += Vector3.up * 0.5f;

        GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);

        // 생성된 아이템을 5초 뒤에 파괴
        Destroy(item, 5f);
    }

    // 내비메시 위의 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 distance 반경 안에서 랜덤한 위치를 찾는다
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance) {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        // 내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;

        // maxDistance 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        // 찾은 점 반환
        return hit.position;
    }
}
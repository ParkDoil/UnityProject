using UnityEngine;

// 게임 오브젝트를 계속 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public float MoveSpeed = 10f; // 이동 속도

    private void Update()
    {
        transform.Translate(-MoveSpeed * Time.deltaTime, 0f, 0f);
    }
}
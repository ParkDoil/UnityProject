using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour
{
    private float _width; // 배경의 가로 길이

    private Vector2 _resetPosition;

    void Awake()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        _width = bc.size.x;

        _resetPosition = new Vector2(_width, 0f);
    }

    void Update()
    {
        if(transform.position.x <= -_width)
        {
            transform.position = _resetPosition;
        }
    }
}
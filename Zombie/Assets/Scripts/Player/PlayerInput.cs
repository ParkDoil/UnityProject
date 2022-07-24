using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour
{
    public string MoveAxisName = "Vertical";
    public string RotateAxisName = "Horizontal";
    public string FireButtonName = "Fire1";
    public string ReloadButtonName = "Reload";

    // 값 할당은 내부에서만 가능
    public float MoveDirection { get; private set; } // 감지된 움직임 입력값
    public float RotateDirection { get; private set; } // 감지된 회전 입력값
    public bool CanFire { get; private set; } // 감지된 발사 입력값
    public bool CanReload { get; private set; } // 감지된 재장전 입력값
    public bool CanMove { get; private set; }

    public Vector3 MousePosition { get; private set; }

    // 매프레임 사용자 입력을 감지
    private void Update()
    {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.Instance.IsGameOver)
        {
            MoveDirection = 0;
            RotateDirection = 0;
            CanFire = false;
            CanReload = false;
            MousePosition = Vector3.zero;
            return;
        }

        // move에 관한 입력 감지
        MoveDirection = Input.GetAxis(MoveAxisName);
        // rotate에 관한 입력 감지
        RotateDirection = Input.GetAxis(RotateAxisName);
        // fire에 관한 입력 감지
        CanFire = Input.GetButton(FireButtonName);
        // reload에 관한 입력 감지
        CanReload = Input.GetButtonDown(ReloadButtonName);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CanMove = Input.GetMouseButton(1);


        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
    }
}
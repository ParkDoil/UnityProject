using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘
    public int MaxJumpCount = 2;

    private int _jumpCount = 0; // 누적 점프 횟수
    private bool _isOnGround = true; // 바닥에 닿았는지 나타냄
    private bool _isDead = false; // 사망 상태

    private Rigidbody2D _rigid;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _zero;

    private static class AnimationID
    {
        public static readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
        public static readonly int DIE = Animator.StringToHash("Die");
    }

    private static readonly float MIN_NORMAL_Y = Mathf.Sin(45f * Mathf.Deg2Rad);

    void Awake() 
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _zero = Vector2.zero;
    }


    void Update()
    {
        if (_isDead)
        {
             return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_jumpCount >=MaxJumpCount)
            {
                return;
            }

            ++_jumpCount;
            _rigid.velocity = _zero;
            _rigid.AddForce(new Vector2(0f, jumpForce));
            _audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_rigid.velocity.y > 0)
            {
                _rigid.velocity *= 0.5f;
            }
        }

        _animator.SetBool(AnimationID.IS_ON_GROUND, _isOnGround);
    }

    void Die()
    {
        _isDead = true;
        _animator.SetTrigger(AnimationID.DIE);
        _rigid.velocity = _zero;
        _audioSource.PlayOneShot(deathClip);

        GameManager.Instance.End();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead")
        {
            if (_isDead == false)
            {
                Die();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D point = collision.GetContact(0);
        if (point.normal.y >= MIN_NORMAL_Y)
        {
            _isOnGround = true;
            _jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _isOnGround = false;
    }
}
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed = 5f;
    public float RotateSpeed = 180f;

    private PlayerInput _input;
    private Rigidbody _rigid;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        move();
        rotate();
        _animator.SetFloat(PlayerAnimID.MOVE, _input.MoveDirection);
    }

    private void move()
    {
        if(_input.CanMove)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
            {
                _navMeshAgent.destination = hit.point;
            }
        }
        else
        {
            Vector3 deltaPosition = MoveSpeed * Time.fixedDeltaTime * _input.MoveDirection * transform.forward;
            _rigid.MovePosition(_rigid.position + deltaPosition);
        }
    }

    private void rotate()
    {
        float rorationAmount = _input.RotateDirection * RotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rorationAmount, 0f);
        _rigid.MoveRotation(_rigid.rotation * deltaRotation);
    }
}
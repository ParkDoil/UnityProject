using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기

// 적 AI를 구현한다
public class Enemy : LivingEntity
{
    public LayerMask TargetLayer; 
    public ParticleSystem HitEffect;
    public AudioClip DeathSound;
    public AudioClip HitSound;
    
    public float Damage = 20f;
    public float AttackCooltime = 0.5f;

    private LivingEntity _target;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AudioSource _audio;
    private Renderer _renderer;

    private float _lastAttackTime;
    private Collider[] _targetCandidates = new Collider[5];
    private int _targetCandidateCount;
    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool _hasTargetFound
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (_target != null && !_target.IsDead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        InitialHealth = newHealth;
        Damage = newDamage;
        _navMeshAgent.speed = newSpeed;
        _renderer.material.color = skinColor;
    }

    private void Start()
    {
        StartCoroutine(updatePath());
    }

    private void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션을 재생
        _animator.SetBool(ZombieAnimID.HASTARGET, _hasTargetFound);
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator updatePath()
    {
        while (IsDead == false)
        {
            if (_hasTargetFound)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_target.transform.position);
            }
            else
            {
                _navMeshAgent.isStopped = true;

                _targetCandidateCount = Physics.OverlapSphereNonAlloc(transform.position, 8f, _targetCandidates, TargetLayer);

                for (int i = 0; i < _targetCandidateCount; ++i)
                {
                    Collider targetCandidate = _targetCandidates[i];

                    LivingEntity livingEntity = targetCandidate.GetComponent<LivingEntity>();
                    Debug.Assert(livingEntity != null);

                    if (livingEntity.IsDead == false)
                    {
                        _target = livingEntity;

                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);

        if(IsDead == false)
        {
            HitEffect.transform.position = hitPoint;
            HitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            HitEffect.Play();

            _audio.PlayOneShot(HitSound);
        }
    }

    // 사망 처리
    public override void Die()
    {
        base.Die();

        _animator.SetTrigger(ZombieAnimID.DIE);
        _audio.PlayOneShot(DeathSound);

        Collider[] ZombieColliders = GetComponents<Collider>();
        for (int i = 0; i < ZombieColliders.Length; ++i)
        {
            ZombieColliders[i].enabled = false;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if(IsDead == false && Time.time >= _lastAttackTime + AttackCooltime)
        {
            LivingEntity livingEntity = other.GetComponent<LivingEntity>();

            if (livingEntity == _target)
            {
                Vector3 HitPosition = other.ClosestPoint(transform.position);
                Vector3 Hitnormal = transform.position - other.transform.position;
                livingEntity.OnDamage(Damage, HitPosition, Hitnormal);

                _lastAttackTime = Time.time;
            }
        }
        
    }
}
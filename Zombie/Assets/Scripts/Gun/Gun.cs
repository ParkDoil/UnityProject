using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    { 
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State CurrentState { get; private set; }

    public Transform FireTransform;

    public ParticleSystem MuzzleFlashEffect;
    public ParticleSystem ShellEjectEffect;

    private LineRenderer _bulletLineRenderer;

    public GunData Data;

    private AudioSource _audioSource;

    private float _fireDistance = 50f;

    private int _remainAmmo;
    private int _ammoInMagazine;

    private float _lastFireTime;

    private void Awake()
    {
        _bulletLineRenderer = GetComponent<LineRenderer>();
        _bulletLineRenderer.positionCount = 2;
        _bulletLineRenderer.enabled = false;

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _remainAmmo = Data.InitalAmmoCount;
        _ammoInMagazine = Data.MagazinCapacity;
        CurrentState = State.Ready;
        _lastFireTime = 0f;
    }

    // 발사 시도
    public void Fire()
    {
        if (CurrentState != State.Ready || Time.time < _lastFireTime + Data.FireCooltime)
        {
            return;
        }

        _lastFireTime = Time.time;
        Shot();
    }

    // 실제 발사 처리
    private void Shot()
    {
        RaycastHit hit;

        Vector3 hitPosition;

        if(Physics.Raycast(FireTransform.position,transform.forward, out hit, _fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if(target != null)
            {
                target.OnDamage(Data.Damage, hit.point, hit.normal);
            }

            hitPosition = hit.point;
        }
        else
        {
            hitPosition = FireTransform.position + transform.forward * _fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        --_ammoInMagazine;
        if (_ammoInMagazine <= 0)
        {
            CurrentState = State.Empty;
        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        MuzzleFlashEffect.Play();
        ShellEjectEffect.Play();

        _audioSource.PlayOneShot(Data.ShotClip);

        _bulletLineRenderer.SetPosition(0, FireTransform.position);
        _bulletLineRenderer.SetPosition(1, hitPosition);
        _bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        _bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool TryReload()
    {
        if (CurrentState == State.Reloading || _remainAmmo <= 0 || _remainAmmo == Data.MagazinCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
         
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        CurrentState = State.Reloading;

        //재장전 소리 재생
        _audioSource.PlayOneShot(Data.ReloadClip);
        
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(Data.ReloadTime);

        //총알 채워야함
        int ammoToFill = Mathf.Min(Data.MagazinCapacity - _ammoInMagazine, _remainAmmo);
        _ammoInMagazine += ammoToFill;
        _remainAmmo -= ammoToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}
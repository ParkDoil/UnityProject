using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public float RotationSpeed = 10f;
    public float AttackCooltime = 0.5f;
    public GameObject BulletPrefab;
    public Transform BulletFireLocation;

    public TurretActivateZone _activateZone;
    public TurretTargeting _targeting;
    private float _elapsedTime;

    void Update()
    {
        if (_activateZone.IsActivate)
        {
            OnLockOn();
        }
        else
        {
            OnIdle();
        }
    }

    void OnIdle()
    {
        _elapsedTime = AttackCooltime;
        transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f);
    }

    void OnLockOn()
    {
        transform.LookAt(_targeting.Target.transform);

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= AttackCooltime)
        {
            _elapsedTime = 0f;
            Instantiate(BulletPrefab, BulletFireLocation.position, BulletFireLocation.rotation);
        }
    }
}
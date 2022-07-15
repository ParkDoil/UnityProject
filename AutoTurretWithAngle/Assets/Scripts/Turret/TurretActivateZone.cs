using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretActivateZone : MonoBehaviour
{
    public bool IsActivate { get; set; }
    public Transform Target;
    public Transform MyBarrelLocation;
    private float _maxAngle = 0.96f;
    private float _minAngle = 0.26f;
    private float _dot;

    private Vector3 _arcStartVector;
    private void Start()
    {
        IsActivate = false;
        _arcStartVector = new Vector3(0f, 0f, 0f);
    }
    public void CheckPlayerInActivateArea(Collider other)
    {
        Target = other.transform;

        if (PlayerFrontEyesight() && PlayerLeft())
        {
            IsActivate = true;
        }
        else
        {
            IsActivate = false;
        }
    }
    private bool PlayerFrontEyesight()
    {
        Vector3 distanceVector = Target.position - MyBarrelLocation.position;
        _dot = Vector3.Dot(MyBarrelLocation.forward, distanceVector.normalized);
        if (_minAngle <= _dot && _maxAngle >= _dot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PlayerLeft()
    {
        Vector3 targetDir = Target.position - MyBarrelLocation.position;
        Vector3 crossVec = Vector3.Cross(targetDir, MyBarrelLocation.forward);

        float dot = Vector3.Dot(crossVec, Vector3.up);

        if (dot > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    private void OnDrawGizmos()
    {
        Handles.color = IsActivate ? _red : _blue;
        Handles.DrawSolidArc(MyBarrelLocation.position, MyBarrelLocation.up, MyBarrelLocation.forward + new Vector3(0.2f, 0f, 0.2f), -60f, 15f);
    }
}
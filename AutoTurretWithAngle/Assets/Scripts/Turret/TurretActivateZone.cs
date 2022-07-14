using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActivateZone : MonoBehaviour
{
    public bool IsActivate { get; set; }
    public Transform Target;
    public Transform MyBarrelLocation;

    private void Start()
    {
        IsActivate = false;
    }
    public void CheckPlayerInActivateArea()
    {
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

        if (0.5f < Vector3.Dot(MyBarrelLocation.forward, distanceVector.normalized))
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
}
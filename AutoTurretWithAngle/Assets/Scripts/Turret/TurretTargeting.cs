using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    public GameObject Target { get; private set; }
    public TurretActivateZone _activateZone;


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _activateZone.CheckPlayerInActivateArea(other);

            Target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _activateZone.IsActivate = false;
        Target = null;
    }
}
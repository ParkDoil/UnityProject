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
            if(_activateZone.IsActivate == false)
            {
                _activateZone.CheckPlayerInActivateArea();
            }
            Target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _activateZone.IsActivate = false;
        Target = null;
    }
}
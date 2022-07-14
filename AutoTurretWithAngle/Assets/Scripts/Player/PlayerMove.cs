using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody _rigid;
    PlayerInput _input;

    public float Speed;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        float xSpeed = _input.X * Speed;
        float zSpeed = _input.Z * Speed;

        _rigid.velocity = new Vector3(xSpeed, 0f, zSpeed);
    }
}
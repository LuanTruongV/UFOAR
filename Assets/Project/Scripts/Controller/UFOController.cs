using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(InputController),typeof(UFOPhysics))]
public class UFOController : MonoBehaviour
{
    private Rigidbody _rb;
    private InputController _inputController;

    private UFOPhysics _physics;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _inputController = GetComponent<InputController>();
        _physics = GetComponent<UFOPhysics>();
    }

    private void FixedUpdate()
    {
        if (_inputController)
        {
            _physics.HanlePhysics(_rb, _inputController);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;
    

    private PlayerMotor _motor;
    //private Gun gun;
    //private WeaponSwitching weaponSwitching;
    private PlayerLook _look;
    private void Awake()
    {
        //weaponSwitching = GameObject.Find("GunHolder").gameObject.GetComponent<WeaponSwitching>();
        //gun = weaponSwitching.weapons[weaponSwitching.selectedWeapon].GetComponent<Gun>();
        
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _motor = gameObject.GetComponent<PlayerMotor>();
        _look = gameObject.GetComponent<PlayerLook>();
        
        _onFoot.Jump.performed += ctx => _motor.Jump();
        _onFoot.Sprint.performed += ctx => _motor.StartSprint();
        _onFoot.Sprint.canceled += ctx => _motor.CancelSprint();
        //_onFoot.Shoot.performed += ctx => gun.Shoot();
    }
    

    private void FixedUpdate()
    {
        _motor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
        
    }

    private void LateUpdate()
    {
        _look.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}

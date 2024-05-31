using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour {

    [SerializeField] private float _speed = 5f;

    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _rb;
    private Vector2 _moveInput;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0;
        _rb.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
    }
}
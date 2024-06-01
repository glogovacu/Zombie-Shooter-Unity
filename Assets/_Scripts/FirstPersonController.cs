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
        RotatePlayer();
        MovePlayer();
    }

    private void RotatePlayer() {
        // Rotate the player based on the camera's X and Y rotation
        Vector3 cameraRotation = _cameraTransform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);

    }

    private void MovePlayer() {
        // Move the player based on input and camera's forward and right directions
        Vector3 move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0; // Ensure the player does not move vertically
        _rb.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2>();
    }
}

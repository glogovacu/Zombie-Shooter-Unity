using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _shootingForce = 20f;

    public static EventHandler OnPlayerShoot;
    public void OnFire(InputAction.CallbackContext context) {
        // Perform a raycast from the camera to the center of the screen
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Determine the target point
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            targetPoint = hit.point;
        } else {
            // If the raycast does not hit anything, use a far away point
            targetPoint = transform.position + transform.forward * 1000f;
        }

        // Calculate the direction to the target point
        Vector3 direction = (targetPoint - _bulletSpawnTransform.position).normalized;

        // Instantiate the projectile at the shooting point
        GameObject projectile = Instantiate(_bullet, _bulletSpawnTransform.position, Quaternion.LookRotation(direction));

        // Get the Rigidbody component of the projectile and apply force
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * _shootingForce, ForceMode.Impulse);
        OnPlayerShoot?.Invoke(this, EventArgs.Empty);
    }
}

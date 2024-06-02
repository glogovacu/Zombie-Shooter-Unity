using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _bulletSpeed = 20f;

    public static EventHandler OnPlayerShoot;
    public void OnFire(InputAction.CallbackContext context) {
        RaycastHit hit;
        Vector3 targetPoint;

        // Cast a ray from the center of the camera
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)) {
            targetPoint = hit.point;
        } else {
            targetPoint = Camera.main.transform.position + Camera.main.transform.forward * 1000f; // Some large value
        }

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null) {
            bulletScript.Initialize(targetPoint);
        }
    }
}

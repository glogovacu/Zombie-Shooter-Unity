using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : StaticInstance<PlayerShooting> { 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private WeaponSwitching _weaponSwitching;

    public EventHandler OnPlayerShoot;
    private float _timeSinceLastShot;
    private bool _isFiring = false;
    private void Update() {
        _timeSinceLastShot += Time.deltaTime;
        if (_isFiring && CanShoot()) {
            Shoot();
        }
     
    }
    public void OnFire(InputAction.CallbackContext context) {
        if (context.performed) {
            _isFiring = true;
        } else if (context.canceled) {
            _isFiring= false;
        }
    }

    private void Shoot() {
        Debug.Log("PerformedShoot");
        
        RaycastHit hit;
        Vector3 targetPoint;

        // Cast a ray from the center of the camera
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000f)) {
            targetPoint = hit.point;
        } else {
            targetPoint = Camera.main.transform.position + Camera.main.transform.forward * 1000f; // Some large value
        }
        OnPlayerShoot?.Invoke(this, EventArgs.Empty);
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnTransform.position, _bulletSpawnTransform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null) {
            bulletScript.Initialize(targetPoint, _weaponSwitching.GetGunData().Damage);
        }
        _timeSinceLastShot = 0f;
    }

    // 600rpm/60s = 10rps 1s/10rps = 0.1s (Time since last shot) i pita da li je tab ukljucen
    private bool CanShoot() => !_weaponSwitching.GetGunData().Reloading && _timeSinceLastShot > 1f / (_weaponSwitching.GetGunData().FireRate / 60f);
}

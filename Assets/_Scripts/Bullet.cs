using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 7000f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private Rigidbody _rb;
    public void Initialize(Vector3 targetPoint) {
        _rb = GetComponent<Rigidbody>();
        Vector3 direction = (targetPoint - transform.position).normalized;
        _rb.AddForce(direction * _speed);
    }

    void OnCollisionEnter(Collision collision) {
        // Instantiate hit effect at the point of impact
        if (_hitEffect != null) {
            Instantiate(_hitEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }

        if (collision.gameObject.TryGetComponent<HealthBar>(out var health)) {
            health.DecreaseHealth(_damage);
            Debug.Log(collision.gameObject.name);
        } else {
            Debug.Log("No Health Attached");
        }

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}

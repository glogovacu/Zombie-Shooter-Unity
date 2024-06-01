using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody _bulletRigidbody;
    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>(); 

    }
    private void Start()
    {
        _bulletRigidbody.AddForce(transform.forward  * 50f); // 50f is some speed later will be a scriptable object

    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}

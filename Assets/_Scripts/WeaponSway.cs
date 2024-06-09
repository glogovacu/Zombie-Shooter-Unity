using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float _intensity = 1.0f;
    [SerializeField] private float _smooth = 3.0f;

    private Quaternion _originRotation;

    void Start() {
        _originRotation = transform.localRotation;
    }

    void Update() {
        UpdateSway();
    }

    private void UpdateSway() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion xAdj = Quaternion.AngleAxis(-_intensity * mouseX, Vector3.up);
        Quaternion yAdj = Quaternion.AngleAxis(_intensity * mouseY, Vector3.right);
        Quaternion targetRotation = _originRotation * xAdj * yAdj;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
    }
}

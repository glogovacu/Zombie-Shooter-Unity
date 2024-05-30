using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Kamera Shake logika
public class CameraShaker : MonoBehaviour
{
    //Treba nam kamera i jacina kojom je pomeramo
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _positionStrength;
    [SerializeField] private Vector3 _rotationStrength;
    private static event Action Shake;
    //ovo je iz plugina
    public static void Invoke()
    {
        Shake?.Invoke();
    }
    //ovo je iz plugina i prica kako da radi program
    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;
    //ovo je logika za plugin
    private void CameraShake()
    {
        _camera.DOComplete();
        _camera.DOShakePosition(0.3f, _positionStrength);
        _camera.DOShakeRotation(0.3f, _rotationStrength);

    }
}

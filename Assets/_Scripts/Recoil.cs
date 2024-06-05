using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Recoil : MonoBehaviour {

    [SerializeField] private CinemachineImpulseSource CinemachineImpulseSource;

    private void Start() {
        CinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        PlayerShooting.Instance.OnPlayerShoot += PlayerShooting_OnPlayerShoot;
    }

    private void PlayerShooting_OnPlayerShoot(object sender, EventArgs e) {
        CinemachineImpulseSource.GenerateImpulse();
    }
}

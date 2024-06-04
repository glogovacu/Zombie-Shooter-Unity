using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public GunData GunData;
    
    private int _currentAmmo;

    private void Start() {
        _currentAmmo = GunData.MagSize;
        PlayerShooting.Instance.OnPlayerShoot += PlayerShooting_OnPlayerShoot;
    }

    private void PlayerShooting_OnPlayerShoot(object sender, EventArgs e) {
        if (_currentAmmo == 0) {
            StartReload();
            return;
        }
        _currentAmmo--;
    }

    public void StartReload() {
        if (!GunData.Reloading && this.gameObject.activeSelf) {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        GunData.Reloading = true;
        yield return new WaitForSeconds(GunData.ReloadTime);
        GunData.CurrentAmmo = GunData.MagSize;
        GunData.Reloading = false;
    }


    private void OnDisable() => GunData.Reloading = false;

}

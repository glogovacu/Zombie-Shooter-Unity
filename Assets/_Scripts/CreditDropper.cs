using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditDropper : MonoBehaviour {

    [SerializeField] private float _creditToDrop;

    private HealthBar _healthBar;

    private void Start() {
        _healthBar = GetComponent<HealthBar>();
        _healthBar.OnDeath += HealthBar_OnDeath;
    }

    private void HealthBar_OnDeath(object sender, EventArgs e) {
        UpgradeSystem.Instance.AddCredit(_creditToDrop);
    }
}

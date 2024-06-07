using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUpgrade : MonoBehaviour {

    private HealthBar _healthBar;

    private void Start() {
        _healthBar = GetComponent<HealthBar>();
        UpgradeSystem.Instance.OnHealthIncrease += UpgradeSystem_OnHealthIncrease;
    }

    private void UpgradeSystem_OnHealthIncrease(object sender, EventArgs e) {
        _healthBar.MaxHealth += 50f * UpgradeSystem.Instance.HealthModifier;
    }
}

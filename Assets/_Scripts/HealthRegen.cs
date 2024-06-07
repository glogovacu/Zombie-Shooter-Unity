using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour {

    [SerializeField] private float _regenRate = 5f; // Health per second
    [SerializeField] private float _regenDelay = 2f; // Delay before starting regen after taking damage

    private HealthBar _healthBar;

    private Coroutine _regenCoroutine;

    private void Start() {
        if (_healthBar == null) {
            _healthBar = GetComponent<HealthBar>();
        }
    }

    private void OnEnable() {
        _healthBar.OnDeath += HandleDeath;
    }

    private void OnDisable() {
        _healthBar.OnDeath -= HandleDeath;
    }

    public void StartRegen() {
        if (_regenCoroutine != null) {
            StopCoroutine(_regenCoroutine);
        }
        _regenCoroutine = StartCoroutine(RegenHealth());
    }

    public void StopRegen() {
        if (_regenCoroutine != null) {
            StopCoroutine(_regenCoroutine);
        }
    }

    private IEnumerator RegenHealth() {
        yield return new WaitForSeconds(_regenDelay);

        while (_healthBar.Health < _healthBar.MaxHealth) {
            _healthBar.IncreaseHealth(_regenRate + _regenRate * UpgradeSystem.Instance.RegenModifier);
            yield return null;
        }
    }

    private void HandleDeath(object sender, EventArgs e) {
        StopRegen();
    }
}

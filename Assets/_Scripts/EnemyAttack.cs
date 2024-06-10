using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float _attackDamage;

    private void Start() {
        GetComponent<EnemyAi>().OnAttack += EnemyAi_OnAttack;
    }

    private void EnemyAi_OnAttack(object sender, EventArgs e) {
        PlayerSingleton.Instance.HealthBar.DecreaseHealth(_attackDamage);
    }
}

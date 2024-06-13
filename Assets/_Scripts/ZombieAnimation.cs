using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

    private Animator _animator;

    private EnemyAi _zombieAi;

    private void Start() {
        _zombieAi = GetComponent<EnemyAi>();
        _animator = GetComponent<Animator>();
        _zombieAi.OnAttack += EnemyAi_OnAttack;
        _zombieAi.OnChase += EnemyAi_OnChase;
    }

    private void EnemyAi_OnChase(object sender, EventArgs e) {
        //_animator.CrossFade("Chase", 0.2f);
        _animator.SetBool("isChasing", true);
    }

    private void EnemyAi_OnAttack(object sender, EventArgs e) {
        //_animator.CrossFade("Attack", 0.2f);
    }
}

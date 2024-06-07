using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float Health = 100f;
    public float MaxHealth = 100f;

    public EventHandler OnDeath;
    public void DecreaseHealth(float damage)
    {
        Health -= damage;
        if(Health <= 0) {
            Die();
        }
    }

    public void IncreaseHealth(float amount) {
        Health = Mathf.Min(Health + amount, MaxHealth);
    }

    private void Die() {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}

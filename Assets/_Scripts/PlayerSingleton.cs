using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : StaticInstance<PlayerSingleton> {

    public Transform PlayerTransform;
    public HealthBar HealthBar;

    protected override void Awake() {
        base.Awake();
        PlayerTransform = transform;
        HealthBar = GetComponent<HealthBar>();
    }
}

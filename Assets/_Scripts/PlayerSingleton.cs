using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : StaticInstance<PlayerSingleton> {

    public Transform PlayerTransform;

    protected override void Awake() {
        base.Awake();
        PlayerTransform = transform;
    }
}

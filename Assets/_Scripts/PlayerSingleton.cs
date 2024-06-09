using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : StaticInstance<PlayerSingleton> {

    public Transform PlayerTransform;

    private void Start() {
        PlayerTransform = transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : StaticInstance<UpgradeSystem> {

    private float _credit;

    public void AddCredit(float credit) {
        _credit += credit;
    }
}

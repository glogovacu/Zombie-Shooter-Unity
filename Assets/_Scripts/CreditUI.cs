using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _creditText;

    private void Start() {
        _creditText.text = UpgradeSystem.Instance.GetCredit().ToString();
        UpgradeSystem.Instance.OnCreditChanged += UpgradeSystem_OnCreditChanged;
    }

    private void UpgradeSystem_OnCreditChanged(object sender, EventArgs e) {
        _creditText.text = UpgradeSystem.Instance.GetCredit().ToString();
    }
}

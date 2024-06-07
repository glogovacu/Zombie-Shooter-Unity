using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystemUI : MonoBehaviour {

    [SerializeField] private List<Button> _buttons;
    [SerializeField] private List<Upgrade> _upgrades;

    private void Start() {
        for (int i = 0; i < _buttons.Count; i++) {
            int index = i;  // Local copy of the loop variable for the closure
            _buttons[i].onClick.AddListener(() => OnUpgradeButtonClicked(index));
        }
    }

    private void OnUpgradeButtonClicked(int index) {
        Upgrade upgrade = _upgrades[index];
        float upgradeCost = UpgradeSystem.Instance.GetUpgradeCost(upgrade);
        if (UpgradeSystem.Instance.TryDecreaseCredit(upgradeCost)) {
            UpgradeSystem.Instance.ApplyUpgrade(upgrade);
            UpdateButtonUI(_buttons[index], upgrade);
        } else {
            Debug.Log("Not enough credits!");
        }
    }

    private void UpdateButtonUI(Button button, Upgrade upgrade) {
        var textMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = (int.Parse(textMeshPro.text) + 1).ToString();
    }
}

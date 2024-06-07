public class UpgradeSystem : StaticInstance<UpgradeSystem> {

    public float DamageModifier = 0;
    public float HealthModifier = 0;
    public float SpeedModifier = 0;
    public float RegenModifier = 0;
    public int MagSizeModifier = 0;

    private float _credit;

    public void AddCredit(float credit) {
        _credit += credit;
    }

    public bool TryDecreaseCredit(float amount) {
        if (_credit < amount) {
            return false;
        }
        _credit -= amount;
        return true;
    }

    public void ApplyUpgrade(Upgrade upgrade) {
        float currentCost = GetUpgradeCost(upgrade);
        if (TryDecreaseCredit(currentCost)) {
            switch (upgrade.Type) {
                case UpgradeType.Damage:
                    DamageModifier++;
                    break;
                case UpgradeType.Health:
                    HealthModifier++;
                    break;
                case UpgradeType.Speed:
                    SpeedModifier++;
                    break;
                case UpgradeType.Regen:
                    RegenModifier++;
                    break;
                case UpgradeType.MagSize:
                    MagSizeModifier++;
                    break;
            }
        }
    }

    public float GetUpgradeCost(Upgrade upgrade) {
        float currentModifier = GetModifierValue(upgrade.Type);
        return upgrade.BaseCost + (currentModifier * upgrade.CostMultiplier);
    }

    private float GetModifierValue(UpgradeType type) {
        switch (type) {
            case UpgradeType.Damage:
                return DamageModifier;
            case UpgradeType.Health:
                return HealthModifier;
            case UpgradeType.Speed:
                return SpeedModifier;
            case UpgradeType.Regen:
                return RegenModifier;
            case UpgradeType.MagSize:
                return MagSizeModifier;
            default:
                return 0;
        }
    }
}
[System.Serializable]
public class Upgrade {
    public float BaseCost;
    public float CostMultiplier;
    public UpgradeType Type;
}

public enum UpgradeType {
    Damage,
    Health,
    Speed,
    Regen,
    MagSize
}

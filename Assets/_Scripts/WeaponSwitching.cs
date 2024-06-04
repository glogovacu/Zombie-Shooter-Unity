using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    //menjanje weapona logika

    public Gun[] Weapons;
    public int SelectedWeaponId = 0;

    [SerializeField] private KeyCode[] _keys;
    [SerializeField] private float _switchTime = 0.2f;
    
    private float _timeSinceLastSwitch = 0f;
    private void Start() {
        //postavlja koliko weapona imamo
        SetWeapons();
        Select(SelectedWeaponId);
        _timeSinceLastSwitch = 0f;

    }
    private void SetWeapons() {
        Weapons = new Gun[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            Weapons[i] = transform.GetChild(i).GetComponent<Gun>();

        }
        if(_keys== null) _keys = new KeyCode[Weapons.Length];
    }
    private void Update() {
        int previousSelectedWeapon = SelectedWeaponId;
        for(int i=0;i<_keys.Length;i++)
        {
            if (Input.GetKeyDown(_keys[i]) && _timeSinceLastSwitch>= _switchTime)
                SelectedWeaponId= i;
        }
        if(previousSelectedWeapon!=SelectedWeaponId)
        {
            Select(SelectedWeaponId);
        }
        _timeSinceLastSwitch += Time.deltaTime;
    }
    private void Select(int weaponIndex) {
        for (int i=0; i< Weapons.Length; i++)
        {
            Weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        _timeSinceLastSwitch= 0f;

    }

    public GunData GetGunData() {
        return Weapons[SelectedWeaponId].GunData;
    }
}

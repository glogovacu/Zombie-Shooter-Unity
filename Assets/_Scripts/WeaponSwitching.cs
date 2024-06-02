using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    //menjanje weapona logika
    [SerializeField] public Transform[] weapons;
    [SerializeField] private KeyCode[] _keys;
    [SerializeField] private float _switchTime;
    [SerializeField] private int _selectedWeaponId = 0;
    private float timeSinceLastSwitch = 0f;
    private void Start()
    {
        //postavlja koliko weapona imamo
        SetWeapons();
        Select(_selectedWeaponId);
        timeSinceLastSwitch = 0f;

    }
    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);

        }
        if(_keys== null) _keys = new KeyCode[weapons.Length];
    }
    private void Update()
    {
        int previousSelectedWeapon = _selectedWeaponId;
        for(int i=0;i<_keys.Length;i++)
        {
            if (Input.GetKeyDown(_keys[i]) && timeSinceLastSwitch>= _switchTime)
                _selectedWeaponId= i;
        }
        if(previousSelectedWeapon!=_selectedWeaponId)
        {
            Select(_selectedWeaponId);
        }
        timeSinceLastSwitch += Time.deltaTime;
    }
    private void Select(int weaponIndex)
    {
        for (int i=0; i< weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        timeSinceLastSwitch= 0f;

    }
}

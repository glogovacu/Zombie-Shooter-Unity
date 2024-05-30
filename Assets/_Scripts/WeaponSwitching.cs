using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    //menjanje weapona logika
    [SerializeField] public Transform[] weapons;
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private float switchTime;
    [SerializeField] public Gun gun;
    private UIManager uIManager;
    public int selectedWeapon;
    private float timeSinceLastSwitch = 0f;
    private void Start()
    {
        //postavlja koliko weapona imamo
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0f;

    }
    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);

        }
        if(keys== null) keys = new KeyCode[weapons.Length];
    }
    private void Update()
    {
        gun = weapons[selectedWeapon].GetComponent<Gun>();
        int previousSelectedWeapon = selectedWeapon;
        for(int i=0;i<keys.Length;i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch>= switchTime)
                selectedWeapon= i;
        }
        if(previousSelectedWeapon!=selectedWeapon)
        {
            Select(selectedWeapon);
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

        //OnWeaponSelected();

    }
    /*private void OnWeaponSelected()
    {
        
    }*/
}

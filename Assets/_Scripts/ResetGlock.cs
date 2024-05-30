using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGlock : MonoBehaviour
{
    private Gun gun;
    //kada se restaruje igrica da se vrati na pocetno
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        gun.gunData.damage = 12;
        gun.gunData.magSize = 18;
        gun.gunData.currentAmmo = 18;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAK : MonoBehaviour
{
    //kada se restaruje igrica da se vrati na pocetno
    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun= GetComponent<Gun>();
        gun.GunData.Damage = 22;
        gun.GunData.MagSize = 30;
        gun.GunData.CurrentAmmo = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

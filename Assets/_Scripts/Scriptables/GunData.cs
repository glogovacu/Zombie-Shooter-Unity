using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string Name;
    [Header("Pucanje")]
    public float Damage;
    public float MaxDistance;
    [Header("Municija")]
    public int CurrentAmmo;
    public int MagSize;
    public float FireRate;
    public float ReloadTime;
    [HideInInspector]
    public bool Reloading;
    
}

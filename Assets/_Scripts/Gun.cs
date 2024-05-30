using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //povezujem gundata sa njim 
    [SerializeField] public GunData gunData;
    public float timeSinceLastShot;
    public GameObject player;
    private void Start()
    {
        //dinamicji nalazi playera
        player = GameObject.Find("PlayerCapsule");



    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }
    //ne mogu da se setim staje ovo xd
    private void OnDisable() => gunData.reloading = false;
    //Krece reload
    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            //ovo je kao delaj
            StartCoroutine(Reload());
            
        }
    }
    //loika za delay
    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }
    // 600rpm/60s = 10rps 1s/10rps = 0.1s (Time since last shot) i pita da li je tab ukljucen
    public bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Bullet Logika (ono sto izlazi iz cevi)
public class BulletProjectile : MonoBehaviour
{
    //Inicijalizacija vfx (efekta)
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    //Inicijalizacija gunData koj ima damage firerate itd
    [SerializeField] private GunData gunData;
    //Inicijalizacija UIManagera koj ima score 
    [SerializeField] UIManager uIManager;
    private Rigidbody bulletRigidbody;
    private void Awake()
    {
        //Da bi mogli da kontrolisemo rigidbodijem koj je u prefabu bullet
        bulletRigidbody = GetComponent<Rigidbody>(); 

    }
    private void Start()
    {
        // inicijalizujemo  uimanager dinamicki i stavljamo brzinu za bullet
        uIManager = FindObjectOfType<UIManager>();
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.transform.GetComponent<IDamagable>();
        if ( other.GetComponent<BulletTarget>() != null )
        {
            // Nesto si pogodio da ima bullet target skriput
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            damagable?.Damage(gunData.damage);
            uIManager.AddScore(gunData.damage);
            
        }
        else
        {
            // Pogodio si bilo sta druo
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

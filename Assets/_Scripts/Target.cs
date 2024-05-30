using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    public float health = 100f;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    
    //Damage logika za targer
    public void Damage(float damage)
    {
        health -= damage;
        //ovo je audio kad primi damage
        int randomIndex = Random.Range(0, audioClips.Length);
        AudioClip randomClip = audioClips[randomIndex];
        audioSource.clip = randomClip;
        audioSource.Play();

        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }
}

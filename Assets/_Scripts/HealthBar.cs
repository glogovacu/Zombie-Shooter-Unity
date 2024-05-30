using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Sve za healthbar
    public Image healthBar;
    public float health = 100f;
    public float maxHealth = 100f;
    public float increaseInterval = 1f;
    public float decreaseAmount = 20f;
    public float increaseHealth = 1f;
    public GameObject gameOverCanvas;
    private float increaseTimer = 0f;

    void Update()
    {
        //Ovo je logika za svakih x sekundi se poveca za x healta
        increaseTimer += Time.deltaTime;
        if (increaseTimer >= increaseInterval)
        {
            //ne moze preko max healtha da predje
            health = Mathf.Min(health + increaseHealth, maxHealth);
            //ovo je graficki deo healthbara
            healthBar.fillAmount = health / maxHealth;
            increaseTimer = 0f;
            Vector3 localScale = healthBar.transform.localScale;
            localScale.x = health / maxHealth;
            healthBar.transform.localScale = localScale;
        }

        if (health <= 0)
        {
            //cim padne ispod nule pauzira se i ide gameover canvas
            Camera mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            
        }
    }

    public void DecreaseHealth()
    {
        //funkcija za smanjivanje healta koristi se u enemy ai logici
        health = Mathf.Max(health - decreaseAmount, 0);
        healthBar.fillAmount = health / maxHealth;
        Vector3 localScale = healthBar.transform.localScale;
        localScale.x = health / maxHealth;
        healthBar.transform.localScale = localScale;
    }
}

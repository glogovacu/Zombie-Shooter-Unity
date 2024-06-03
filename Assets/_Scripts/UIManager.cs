using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    //Cela ui logika
    [SerializeField]private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Gun gun;
    public TMP_Text ScoreResetText;
    public TMP_Text ScoreText;
    public TMP_Text CreditText;
    public TMP_Text DamageText;
    public TMP_Text MagSizeText;
    public TMP_Text SpeedText;
    public TMP_Text HealthUpgradeText;
    public TMP_Text HealthRegText;
    public TMP_Text ErrorText;
    public float score = 0;
    public float credit = 0;
    public float damageModifier = 0;
    public float speedModifier = 1;
    public float healtModifier = 1;
    public int magSizeModifier = 1;
    public GameObject upgradeCanvas;
    public Target target;
    public HealthBar healthBar;

    //ovo je funkciaj u updejtu koja updejtuje nesto konstanto da bi se videlo
    private void UpdateAmmo()
    {
    }
    //Funkcija koja se poziva za skore i kredit
    public void AddScore(float newScore)
    {
        score+= newScore;
        credit+=newScore;
        ScoreText.text = "Score: " + score.ToString();
        CreditText.text = "Credit: " + credit.ToString();
        ScoreResetText.text = "Highscore: " + score.ToString();
    }
    //Funckija koja se poziva za smanjivanje kredita
    public void DecreaseCredit(float newCredit)
    {
        credit -= newCredit;
        CreditText.text = "Credit: " + credit.ToString();
    }
    //ovde krece logika za ojacavanje lika gde su svi namanjeni za on click
    public void IncreaseDamage()
    {
        if(credit > 20)
        { 
            gun.gunData.damage = gun.gunData.damage + damageModifier;
            DamageText.text = "Damage: " + gun.gunData.damage.ToString();
            DecreaseCredit(20);
        }
        else
        {
            ErrorText.text = "Nemate dovoljno kredita";
        }

    }
    
    public void IncreaseMagSize()
    {
        if (credit > 20)
        {
            gun.gunData.magSize = gun.gunData.magSize + magSizeModifier;
            MagSizeText.text = "Mag Size: " + gun.gunData.magSize.ToString();
            DecreaseCredit(20);
        }
        else
        {
            ErrorText.text = "Nemate dovoljno kredita";
        }
    }
    public void IncreaseSpeed()
    {
        if (credit > 20)
        {
            DecreaseCredit(20);
        }
        else
        {
            ErrorText.text = "Nemate dovoljno kredita";
        }
    }
    public void IncreaseHealth()
    {
        if (credit > 20)
        {
            healthBar.MaxHealth = healthBar.MaxHealth + 20;
            HealthUpgradeText.text = "Max HP: " + healthBar.MaxHealth.ToString();
            DecreaseCredit(20);
        }
        else
        {
            ErrorText.text = "Nemate dovoljno kredita";
        }
    }

    private void Awake()
    {
        damageModifier = 1;
        magSizeModifier = 1;
        credit= 0;
        score = 0;
    }
    private void Update()
    {
        //kad imamo vise od 2000 scora zombiji su jaci duplo
        if(score > 2000)
        {
            target.health = 200f;
        }    
        UpdateAmmo();
    }
}

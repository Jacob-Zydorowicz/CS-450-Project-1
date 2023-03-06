using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthObserver : MonoBehaviour, Observer
{
    Subject subject;
    TextMeshProUGUI healthText;
    HealthBarHandler hbh;
    int maxHealth = -999;
    private void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        subject = GameObject.FindObjectOfType<GameSubject>();
        subject.Register(this);
        hbh = GetComponentInChildren<HealthBarHandler>();
    }
    public void statUpdate(int health, int wave, int enemyCount, int bulletCount, string currentWeapon)
    {
        if (maxHealth == -999 && health != 0)
            maxHealth = health;
        if (healthText != null)
            healthText.text = health.ToString();
        if (hbh != null)
            hbh.UpdateFill(health, maxHealth);
    }
}

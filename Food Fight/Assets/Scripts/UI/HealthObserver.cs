using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthObserver : MonoBehaviour, Observer
{
    Subject subject;
    Text healthText;
    private void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        subject = GameObject.FindObjectOfType<GameSubject>();
        subject.Register(this);
    }
    public void statUpdate(int health, int wave, int enemyCount, int bulletCount, string currentWeapon)
    {
        if (healthText != null)
            healthText.text = "Health: " + health;
    }
}

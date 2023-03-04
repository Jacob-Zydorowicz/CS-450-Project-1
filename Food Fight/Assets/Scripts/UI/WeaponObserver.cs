using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponObserver : MonoBehaviour, Observer
{
    Subject subject;
    TextMeshProUGUI weaponText, ammoText;
    private void Start()
    {
        weaponText = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<TextMeshProUGUI>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<TextMeshProUGUI>();
        subject = GameObject.FindObjectOfType<GameSubject>();
        subject.Register(this);
    }
    public void statUpdate(int health, int wave, int enemyCount, int bulletCount, string currentWeapon)
    {
        if (weaponText != null)
            weaponText.text = "Current weapon: " + currentWeapon;
        if (ammoText != null)
            ammoText.text = "Ammo Left: " + bulletCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubject : MonoBehaviour, Subject
{
    List<Observer> observers;
    int health, wave, enemyCount, bulletCount;
    string currentWeapon;
    public static GameSubject sceneInstance;

    private void Awake()
    {
        sceneInstance = this;
        observers = new List<Observer>();
    }
    public void Notify()
    {
        foreach (Observer ob in observers)
        {
            ob.statUpdate(health,  wave,  enemyCount,  bulletCount,  currentWeapon);
        }
    }

    public void Register(Observer ob)
    {
        observers.Add(ob);
        Notify();
    }

    public void Unregister(Observer ob)
    {
        observers.Remove(ob);
        Notify();
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        health = currentHealth;
        Notify();
    }

    public void UpdateEnemies(int currentEnemies)
    {
        enemyCount = currentEnemies;
        Notify();
    }

    public void UpdateWave(int currentWave)
    {
        wave = currentWave;
        Notify();
    }

    public void UpdateAmmo(int currentAmmo)
    {
        bulletCount = currentAmmo;
        Notify();
    }

    public void UpdateWeapon(string currentWeapon)
    {
        this.currentWeapon = currentWeapon;
        Notify();
    }

}

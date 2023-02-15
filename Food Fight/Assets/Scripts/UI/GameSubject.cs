using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSubject : MonoBehaviour, Subject
{
    List<Observer> observers;
    int health, wave, enemyCount, bulletCount;
    string currentWeapon;

    private void Awake()
    {
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

    public void UpdateHealth(int temp)
    {
        health = temp;
        Notify();
    }

    public void UpdateEnemies(int temp)
    {
        enemyCount = temp;
        Notify();
    }

    public void UpdateWave(int temp)
    {
        wave = temp;
        Notify();
    }

    public void UpdateAmmo(int temp)
    {
        bulletCount = temp;
        Notify();
    }

    public void UpdateWeapon(string temp)
    {
        currentWeapon = temp;
        Notify();
    }

}

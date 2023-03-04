using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyObserver : MonoBehaviour, Observer
{
    Subject subject;
    TextMeshProUGUI enemyText, waveText;
    private void Start()
    {
        enemyText = GameObject.FindGameObjectWithTag("EnemyText").GetComponent<TextMeshProUGUI>();
        waveText = GameObject.FindGameObjectWithTag("WaveText").GetComponent<TextMeshProUGUI>();
        subject = GameObject.FindObjectOfType<GameSubject>();
        subject.Register(this);
    }
    public void statUpdate(int health, int wave, int enemyCount, int bulletCount, string currentWeapon)
    {
        if (enemyText != null)
            enemyText.text = "Enemies left: " + enemyCount;
        if (waveText != null)
            waveText.text = "Wave " + wave;
    }
}

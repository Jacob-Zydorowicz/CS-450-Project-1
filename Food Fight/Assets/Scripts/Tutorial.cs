using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] GameObject enemyPrefab;
    private int textSwitcher = 0;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "Oh No! A food fight has broken out in the school! You must use your skills as a food fighter to survive the day. Move around to avoid the food using W A S D.";
        textSwitcher = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (textSwitcher)
        {
            case 0:
                if((Input.GetKey(KeyCode.W))|| (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))
                {
                    textSwitcher = 1;
                    tutorialText.text = "Hold down or click the LMB to use your Food's primary fire. Click RMB to use your food's secondary fire.";
                }
                break;
            case 1:
                if ((Input.GetMouseButtonDown(0)) || (Input.GetMouseButtonDown(1)))
                {
                    textSwitcher = 2;
                    tutorialText.text = "Press 1 or 2 to switch between your current weapons";
                }
                break;
            case 2:
                if ((Input.GetKey(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.Alpha2)))
                {
                    textSwitcher = 3;
                    tutorialText.text = "Use your weapon to lower the enemies health to 0";
                    Instantiate(enemyPrefab, new Vector3(1, 1, 0), Quaternion.identity);
                }
                break;
            case 3:
                if (!GameObject.FindGameObjectWithTag("Enemy"))
                {
                    Instantiate(enemyPrefab, new Vector3(1, 1, 1), Quaternion.identity);
                    textSwitcher = 4;
                }
                   
                break;
            case 4:
                if(!GameObject.FindGameObjectWithTag("Enemy"))
                    Instantiate(enemyPrefab, new Vector3(1, 1, 1), Quaternion.identity);
                tutorialText.text = "Press space to continue to the game. Good Luck";

                if (Input.GetKey(KeyCode.Space))
                    SceneManager.LoadScene("Classroom level");
                break;
        }
    }
}

/*
 * (Jacob Welch)
 * (ChargeBarHandler)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarHandler : MonoBehaviour
{
    #region Fields
    private static ChargeBarHandler instance;

    public Image ChargeBarImage { get; private set; }
    #endregion

    #region Functions
    /// <summary>
    /// Handles initilization of components and other fields before anything else.
    /// </summary>
    private void Awake()
    {
        instance = this;
        ChargeBarImage = GetComponent<Image>();
        UpdateFill(0);
    }

    public static void UpdateFill(float currentFill)
    {
        instance.ChargeBarImage.fillAmount = currentFill;
        instance.transform.parent.gameObject.SetActive(currentFill != 0);
    }
    #endregion
}

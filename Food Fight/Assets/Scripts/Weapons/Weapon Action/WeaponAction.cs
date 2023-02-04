/*
 * (Jacob Welch)
 * (WeaponAction)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAction : MonoBehaviour
{
    #region Fields
    protected Weapon weapon;
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public abstract void PerformAction();
    #endregion
}

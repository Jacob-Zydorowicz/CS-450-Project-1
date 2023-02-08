/*
 * (Jacob Welch)
 * (BoomerangProjectile)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    #region Fields

    #endregion

    #region Functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //weapon.
        }
    }
    #endregion
}

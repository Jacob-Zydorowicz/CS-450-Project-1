/*
 * (Jacob Welch)
 * (ProjectileWeaponAction)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponAction : WeaponAction
{
    #region Fields
    [Tooltip("Projectile to be spawned by the weapon")]
    [SerializeField] private GameObject projectile;
    #endregion

    #region Functions
    public override void PerformAction()
    {
        var spawnedProjectile = Instantiate(projectile, transform.position, transform.rotation);
        spawnedProjectile.GetComponent<Projectile>().Initialize(weapon.Range);
    }
    #endregion
}

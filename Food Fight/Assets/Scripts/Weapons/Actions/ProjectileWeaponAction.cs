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

    [field: Range(0.0f, 10000.0f)]
    [field: Tooltip("The speed of the projectile")]
    [field: SerializeField] public float ProjectileSpeed { get; private set; } = 5.0f;
    #endregion

    #region Functions
    public override void PerformAction(float damageMod = 1, float speedMod = 1, float rangeMod = 1)
    {
        base.PerformAction(damageMod, speedMod, rangeMod);

        // Spawns and initializes the projectile
        var spawnedProjectile = Instantiate(projectile, transform.position, transform.rotation * Spread());
        spawnedProjectile.GetComponent<Projectile>().Initialize(Range*rangeMod, (int)(Damage*damageMod), ProjectileSpeed * speedMod);
    }
    #endregion
}

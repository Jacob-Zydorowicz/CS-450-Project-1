/*
 * (Jacob Welch)
 * (ExplodingProjectile)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : Projectile
{
    #region Fields
    [Range(0, 100)]
    [Tooltip("The number of projectiles that explode out from this projectile")]
    [SerializeField] private int numberOfExplodedProjectiles = 20;

    [Range(0, 100)]
    [Tooltip("The number of exploded projectiles before adding a spacing delay")]
    [SerializeField] private int amountBeforeSpacing = 10;

    [Range(0.0f, 1.0f)]
    [Tooltip("The amount of time to delay for spacing")]
    [SerializeField] private float spacingDelay = 0.05f;

    [Range(0, 100)]
    [Tooltip("The amount of time to delay for spacing")]
    [SerializeField] private int explodedProjectilDamage = 5;

    [Range(0, 100)]
    [Tooltip("The amount of time to delay for spacing")]
    [SerializeField] private float expodedProjectileRange = 10;

    [SerializeField] private GameObject explodedProjectile;

    [Tooltip("The sound played when this object explodes")]
    [SerializeField] private AudioClip explosionSound;

    [Range(0.0f, 1.0f)]
    [Tooltip("Explosion sound volume")]
    [SerializeField] private float explosionSoundVolume = 1.0f;
    #endregion

    #region Functions
    protected override IEnumerator DestructionEvent()
    {
        SoundManager.PlaySound(explosionSound, explosionSoundVolume, transform.position);

        for(int i = 1; i <= numberOfExplodedProjectiles; i++)
        {
            Instantiate(explodedProjectile, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f, 360.0f)))).GetComponent<Projectile>().Initialize(expodedProjectileRange, explodedProjectilDamage);

            if(i%amountBeforeSpacing == 0)
            {
                yield return new WaitForSeconds(spacingDelay);
            }
        }

        yield return base.DestructionEvent();
    }
    #endregion
}

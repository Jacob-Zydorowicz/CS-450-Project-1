/*
 * (Jacob Welch)
 * (FullyautoWeaponRoutine)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using UnityEngine;

public class FullyautoWeaponRoutine : WeaponRoutine
{
    #region Fields
    [Tooltip("Holds reference to the users input if they click slightly before being able to shoot again")]
    [SerializeField] private float holdInputReferenceTime = 0.2f;
    #endregion

    #region Functions
    public override void WeaponDown()
    {
        base.WeaponDown();
        StartCoroutine(ShootingRoutine());
    }

    public override void WeaponUp()
    {
        base.WeaponUp();

        if (hasInput)
        {
            StartCoroutine(HoldInputRefence());
        }
    }

    protected IEnumerator ShootingRoutine()
    {
        while (hasInput)
        {
            if (weapon.canShoot && ShotTimeAllowed())
            {
                PerformWeaponAction();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator HoldInputRefence()
    {
        var t = holdInputReferenceTime;

        while (t > 0)
        {
            yield return new WaitForEndOfFrame();

            if (ShotTimeAllowed() && weapon.canShoot)
            {
                PerformWeaponAction();
                yield break;
            }

            t -= Time.deltaTime;
        }
    }
    #endregion
}

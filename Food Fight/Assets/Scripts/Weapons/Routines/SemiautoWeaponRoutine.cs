/*
 * (Jacob Welch)
 * (SemiautoWeaponRoutine)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiautoWeaponRoutine : WeaponRoutine
{
    #region Fields
    [Tooltip("Holds reference to the users input if they click slightly before being able to shoot again")]
    [SerializeField] private float holdInputReferenceTime = 0.2f;
    #endregion

    #region Functions
    public override void WeaponDown()
    {
        base.WeaponDown();

        if (weapon.canShoot && ShotTimeAllowed())
        {
            PerformWeaponAction();
        }
        else
        {
            StartCoroutine(HoldInputRefence());
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

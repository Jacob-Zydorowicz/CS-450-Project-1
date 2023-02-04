/*
 * (Jacob Welch)
 * (SemiAutoWeapon)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : Weapon
{
    #region Fields
    [Tooltip("Holds reference to the users input if they click slightly before being able to shoot again")]
    [SerializeField] private float holdInputReferenceTime = 0.2f;

    private bool canShoot = true;
    #endregion

    #region Functions
    public override void MainActionDown()
    {
        if (HasAmmo())
        {
            if (canShoot)
            {
                PerformWeaponAction();
            }
            else
            {
                StartCoroutine(HoldInputRefence());
            }
        }
    }

    protected override void PerformWeaponAction()
    {
        base.PerformWeaponAction();
        StartCoroutine(DelayNextShot());
    }

    private IEnumerator DelayNextShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(TimeBetweenShots);
        canShoot = true;
    }

    private IEnumerator HoldInputRefence()
    {
        var t = holdInputReferenceTime;

        while(t > 0)
        {
            yield return new WaitForEndOfFrame();

            if (canShoot)
            {
                PerformWeaponAction();
                yield break;
            }

            t -= Time.deltaTime;
        }
    }

    public override void MainActionUp()
    {

    }

    protected override IEnumerator Reload()
    {
        yield return base.Reload();

        canShoot = true;
    }
    #endregion
}

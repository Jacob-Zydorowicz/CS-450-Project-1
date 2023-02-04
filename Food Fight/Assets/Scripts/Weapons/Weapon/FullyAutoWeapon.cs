/*
 * (Jacob Welch)
 * (FullyAutoWeapon)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using UnityEngine;

public class FullyAutoWeapon : Weapon
{
    #region Fields
    [Tooltip("Holds reference to the users input if they click slightly before being able to shoot again")]
    [SerializeField] private float holdInputReferenceTime = 0.2f;

    private float lastShotFired = -Mathf.Infinity;

    private Coroutine shootingRoutine;
    #endregion

    #region Functions
    public override void MainActionDown()
    {
        shootingRoutine = StartCoroutine(ShootingRoutine());
    }

    public override void MainActionUp()
    {
        if (shootingRoutine != null) StopCoroutine(shootingRoutine);
        shootingRoutine = null;

        if (HasAmmo())
        {
            StartCoroutine(HoldInputRefence());
        }
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            if (CanShoot() && HasAmmo())
            {
                PerformWeaponAction();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected override void PerformWeaponAction()
    {
        base.PerformWeaponAction();
        lastShotFired = Time.time;
    }

    private bool CanShoot()
    {
        return TimeSinceLastShot() > TimeBetweenShots;
    }

    private float TimeSinceLastShot()
    {
        return Time.time - lastShotFired;
    }

    private IEnumerator HoldInputRefence()
    {
        var t = holdInputReferenceTime;

        while (t > 0)
        {
            yield return new WaitForEndOfFrame();

            if (CanShoot())
            {
                PerformWeaponAction();
                yield break;
            }

            t -= Time.deltaTime;
        }
    }

    protected override IEnumerator Reload()
    {
        yield return base.Reload();

        lastShotFired = -Mathf.Infinity;
    }
    #endregion
}

/*
 * (Jacob Welch)
 * (ChargeWeaponRoutine)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeaponRoutine : WeaponRoutine
{
    #region Fields
    [SerializeField] private float chargeTime = 1.0f;

    [Header("Automatic Release")]
    [SerializeField] private bool isAutomaticRelease = false;
    [SerializeField] private float timeBeforeAutomaticRelease = 1.0f;

    [Header("Must Fully Charge")]
    [SerializeField] private bool mustFullyCharge = false;
    [SerializeField] private float delayUponRelease = 1.0f;

    [Header("Continued Full Charge")]
    [SerializeField] private bool continueChargeUponRelease = false;

    private float currentCharge = 0;
    public float CurrentCharge
    {
        get => currentCharge;
        private set
        {
            currentCharge = value;
            ChargeBarHandler.UpdateFill(currentCharge);
        }
    }

    private Coroutine automaticReleaseRoutineRef;

    [Header("Charge Weapon Modifiers")]
    [SerializeField] private float chargeDamageMod = 2.0f;
    [SerializeField] private float chargeSpeedMod = 2.0f;
    [SerializeField] private float chargeRangeMod = 2.0f;
    #endregion

    #region Functions
    #region Charge
    public override void WeaponDown()
    {
        base.WeaponDown();

        if (weapon.canShoot && ShotTimeAllowed())
        {
            StartChargeRoutine();
        }
        else
        {
            StartCoroutine(HoldInputReference());
        }
    }

    private IEnumerator HoldInputReference()
    {
        while (hasInput)
        {
            yield return new WaitForFixedUpdate();

            if (weapon.canShoot && ShotTimeAllowed())
            {
                StartChargeRoutine();
                yield break;
            }
        }
    }

    private void StartChargeRoutine()
    {
        weapon.nextShotAllowedTime = Mathf.Infinity;
        StartCoroutine(ChargeRoutine());
    }

    /// <summary>
    /// Handles the routine for spells that can be charged up.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ChargeRoutine()
    {
        while (hasInput || (continueChargeUponRelease && CurrentCharge != 1.0f))
        {
            yield return new WaitForFixedUpdate();

            IncrementCharge();
        }

        if (mustFullyCharge)
        {
            if(CurrentCharge == 1.0f)
            {
                PerformWeaponAction(DamageMod(), SpeedMod(), RangeMod());
            }
            else
            {
                weapon.nextShotAllowedTime = Time.time + delayUponRelease;
            }
        }
        else if(CurrentCharge != 0.0f)
        {
            PerformWeaponAction(DamageMod(), SpeedMod(), RangeMod());
        }

        StartAutomaticRelease(false);
        CurrentCharge = 0;
    }

    /// <summary>
    /// Increments the current amount for the holding of charge.
    /// </summary>
    protected virtual void IncrementCharge()
    {
        if (CurrentCharge != 1.0f)
        {
            CurrentCharge += Time.fixedDeltaTime / chargeTime;
            CurrentCharge = Mathf.Clamp(CurrentCharge, 0.0f, 1.0f);

            if (isAutomaticRelease && CurrentCharge == 1.0f)
            {
                StartAutomaticRelease(true);
            }
        }
    }

    private float DamageMod()
    {
        return Mathf.Lerp(1.0f, chargeDamageMod, CurrentCharge);
    }

    private float SpeedMod()
    {
        return Mathf.Lerp(1.0f, chargeSpeedMod, CurrentCharge);
    }

    private float RangeMod()
    {
        return Mathf.Lerp(1.0f, chargeRangeMod, CurrentCharge);
    }

    #region Automatic Release
    /// <summary>
    /// Starts or stops the process of automatically relaseing the spell.
    /// </summary>
    /// <param name="shouldStart"></param>
    private void StartAutomaticRelease(bool shouldStart)
    {
        if (shouldStart)
        {
            automaticReleaseRoutineRef = StartCoroutine(AutomaticReleaseRoutine());
        }
        else if (automaticReleaseRoutineRef != null)
        {
            StopCoroutine(automaticReleaseRoutineRef);
        }
    }

    /// <summary>
    /// Waits before automatically releasing the spell action.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator AutomaticReleaseRoutine()
    {
        yield return new WaitForSeconds(timeBeforeAutomaticRelease);
        automaticReleaseRoutineRef = null;
        hasInput = false;
    }
    #endregion
    #endregion
    #endregion
}

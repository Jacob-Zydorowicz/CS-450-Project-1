/*
 * (Jacob Welch)
 * (PlayerHealthController)
 * (Food Fight)
 * (Description: controls the event of losing all your health as the player.)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : Damageable
{
    #region Functions
    /// <summary>
    /// The event that happens when this damageable loses all of its health.
    /// </summary>
    protected override void DestructionEvent()
    {
        print("You Are Dead!!!");
    }
    #endregion
}

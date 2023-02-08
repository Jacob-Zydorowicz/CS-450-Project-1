/*
 * (Jacob Welch)
 * (Projectile)
 * (Food Fight)
 * (Description: )
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ProjectileMovementPattern))]
public class Projectile : MonoBehaviour
{
    #region Fields
    [Range(0.0f, 50.0f)]
    [Tooltip("The speed of the projectile")]
    [SerializeField] private float speed = 5;

    private float range = 0;

    protected WeaponAction weapon;
    private ProjectileMovementPattern movementPattern;
    #endregion

    #region Functions
    private void Reset()
    {
        var rigidbody = GetComponent<Rigidbody2D>();

        // Initializes all projectiles to have the same rigidbody settings
        rigidbody.gravityScale = 0.0f;
        rigidbody.angularDrag = 0.0f;
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    public void Initialize(WeaponAction weapon)
    {
        range = weapon.Range;
        this.weapon = weapon;
        movementPattern = GetComponent<ProjectileMovementPattern>();
    }

    private void FixedUpdate()
    {
        MovementPattern();
    }

    protected virtual void MovementPattern()
    {
        movementPattern.MovementTick(speed, range);
    }

    protected virtual IEnumerator DestructionEvent()
    {
        Destroy(gameObject);
        yield return null;
    }

    public virtual void MaxRangeReached()
    {
        StartCoroutine(DestructionEvent());
    }
    #endregion
}

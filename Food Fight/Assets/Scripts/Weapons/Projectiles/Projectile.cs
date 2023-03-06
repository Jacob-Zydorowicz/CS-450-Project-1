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
    private string ignoreTag = "Player";

    #region Stats
    [Range(0.0f, 50.0f)]
    [Tooltip("The speed of the projectile")]
    [SerializeField] private float speed = 5;

    /// <summary>
    /// The amount of damage that this projectile can deal.
    /// </summary>
    private int damage = 1;

    private float range = 0;
    #endregion

    #region Sound
    [Tooltip("The sound played when hitting something")]
    [SerializeField] private AudioClip hitSound;

    [Range(0.0f, 1.0f)]
    [Tooltip("Hit sound volume")]
    [SerializeField] private float hitSoundVolume = 1.0f;
    #endregion

    [Tooltip("The vfx spawned at the location that this projectile hits")]
    [SerializeField] private GameObject hitVFX;

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

    public void Initialize(float range, int damage, string ignoreTag = "Player")
    {
        this.range = range;
        this.damage = damage;
        this.ignoreTag = ignoreTag;
        movementPattern = GetComponent<ProjectileMovementPattern>();
    }

    public void Initialize(float range, int damage, float bulletSpeed, string ignoreTag = "Player")
    {
        this.range = range;
        this.damage = damage;
        speed = bulletSpeed;

        this.ignoreTag = ignoreTag;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ShouldIgnore(collision.gameObject) && collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            HitEvent(damageable);
            StartCoroutine(DestructionEvent());
        }
    }

    private void HitEvent(Damageable damageable)
    {
        SpawnHitVFX();
        SoundManager.PlaySound(hitSound, hitSoundVolume, transform.position);
        damageable.UpdateHealth(damage);
    }

    private void SpawnHitVFX()
    {
        if (hitVFX == null) return;

        var tempHitVFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(tempHitVFX, 2.0f);
    }

    protected bool ShouldIgnore(GameObject obj)
    {
        return obj.CompareTag(ignoreTag);
    }
    #endregion
}

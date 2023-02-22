using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    [SerializeField] private float projectileRange = 4;
    [SerializeField] private int damage;

    [SerializeField] private float movementSpeed = 3;

    protected string type;

    public GameObject player;

    [SerializeField] private float lineOfSite = 20;
    [SerializeField] private float shootingRange = 4;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime;


    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectileParent;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        var currentDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (currentDistance < lineOfSite && currentDistance > shootingRange && nextFireTime < Time.time)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else if (currentDistance <= shootingRange && nextFireTime < Time.time)
        {
            if (projectileParent == null) return;

            var spawnedProjectile = Instantiate(projectile, projectileParent.transform.position, Quaternion.Euler(Vector3.forward * angle));
            spawnedProjectile.GetComponent<Projectile>().Initialize(projectileRange, damage, "Enemy");
            nextFireTime = Time.time + fireRate;
        }
    }
}

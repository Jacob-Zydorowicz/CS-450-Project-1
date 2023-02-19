using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    public float damage;
    public float speed;

    public GameObject player;

    private float distance;

    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;

    public GameObject projectile;
    public GameObject projectileParent;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        speed = 1;
        lineOfSite = 4;
        shootingRange = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < lineOfSite && distance > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else if (distance <= shootingRange && nextFireTime < Time.time)
        {
            if (projectileParent == null) return;

            Instantiate(projectile, projectileParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}

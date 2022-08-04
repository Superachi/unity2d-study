using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerController playerController;

    public float cooldownRemaining;
    public float cooldownSpeed;
    public GameObject bulletPrefab;
    public bool isShooting;
    public bool canShoot;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    public void Attack(Vector2 originPosition, Vector2 targetPosition, float cooldown)
    {
        CreateBullet(originPosition, targetPosition);
        cooldownRemaining = cooldown;
        isShooting = true;
    }

    public void CreateBullet(Vector2 originPosition, Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();

        Vector2 direction = targetPosition - originPosition;
        bulletBehaviour.SetMechanics(1, gameObject);
        bulletBehaviour.SetTrajectory(direction.normalized, 20f, 0.3f);
        bulletBehaviour.SetOrientation(true, false);

        // Need to set the position of the bullet after setting its properties
        // Otherwise the projectile looks off for a single frame (wrong position/angle)
        bullet.transform.position = AngleCalc.LengthDirection(0.5f, direction, originPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownRemaining > 0)
        {
            canShoot = false;
            cooldownRemaining -= Time.deltaTime;
        }
        else
        {
            if (!playerController.fireButtonPressed) isShooting = false;
            canShoot = true;
        }
    }
}

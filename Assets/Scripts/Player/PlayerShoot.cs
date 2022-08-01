using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float cooldownRemaining;
    public float cooldownSpeed;
    public GameObject bulletPrefab;

    public void CreateBullet(Vector2 originPosition, Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        Vector2 direction = targetPosition - originPosition;

        // Need to set the position of the bullet after setting its properties
        // Otherwise the projectile looks off for a single frame (wrong position/angle)
        bulletBehaviour.SetBulletTrajectory(direction.normalized, 20f);
        bulletBehaviour.SetBulletAesthetics(true, true);
        bullet.transform.position = originPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

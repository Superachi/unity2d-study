using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Mechanics
    public float damage = 1f;
    public GameObject shooter;
    private Creature shooterCreature;

    // Trajectory
    public Vector2 moveDirection = Vector2.right;
    public float moveSpeed = 20f;
    public float lifetime = 3f;

    // Aesthetics
    public float facingDirection = AngleCalc.ANGLE_RIGHT;
    public bool isFacingDirection = true;
    public bool isFacingDiagonally = false;

    // Initialiser methods
    public void SetMechanics(float _damage, GameObject _shooter)
    {
        damage = _damage;
        shooter = _shooter;
        shooterCreature = shooter.GetComponent<Creature>();
    }

    public void SetTrajectory(Vector2 direction, float speed, float _lifetime)
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = direction;
        moveSpeed = speed;
        lifetime = _lifetime;
    }

    public void SetOrientation(bool faceDir, bool faceDiag)
    {
        isFacingDirection = faceDir;
        isFacingDiagonally = faceDiag;
        RotateSprite();
    }

    // Update methods

    private void RotateSprite()
    {
        if (isFacingDirection)
        {
            facingDirection = AngleCalc.AngleBetweenPoints(Vector2.zero, moveDirection);
        }

        float tempDirection = facingDirection - 90;
        if (isFacingDiagonally) tempDirection = facingDirection - 45;

        Quaternion rotation = Quaternion.AngleAxis(tempDirection, Vector3.back);
        transform.rotation = rotation;
    }

    private void Lifetime()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Collision methods
    private void Hit(GameObject hitObject)
    {
        if (hitObject.CompareTag("Bullet")) return;
        if (hitObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        Creature colliderCreature = hitObject.GetComponent<Creature>();
        if (shooterCreature.type != colliderCreature.type)
        {
            colliderCreature.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.gameObject);
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        RotateSprite();
        Lifetime();
    }
}

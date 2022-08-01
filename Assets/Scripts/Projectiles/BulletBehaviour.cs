using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Trajectory
    public Vector2 moveDirection = Vector2.right;
    public float moveSpeed = 20f;
    public float lifetime = 3f;

    // Aesthetics
    public float facingDirection = AngleCalculation.ANGLE_RIGHT;
    public bool isFacingDirection = true;
    public bool isFacingDiagonally = false;

    public void SetBulletTrajectory(Vector2 direction, float speed, float _lifetime)
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = direction;
        moveSpeed = speed;
        lifetime = _lifetime;
    }

    public void SetBulletOrientation(bool faceDir, bool faceDiag)
    {
        isFacingDirection = faceDir;
        isFacingDiagonally = faceDiag;
        RotateSprite();
    }

    private void RotateSprite()
    {
        if (isFacingDirection)
        {
            facingDirection = AngleCalculation.AngleBetweenPoints(Vector2.zero, moveDirection);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        RotateSprite();
        Lifetime();
    }
}

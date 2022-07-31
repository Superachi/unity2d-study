using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    // Movement
    public Vector2 moveDirection;
    public float moveSpeed = 10f;
    public float moveDegrees = 90;
    public bool isMoving = false;

    // Collision handling
    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

    public bool MovePlayer(Vector2 direction)
    {
        // We did not move if our directional Vector2 is (0, 0), so return false
        if (direction == Vector2.zero) return false;

        if (isDirectionFree(direction))
        {
            rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
            isMoving = true;
            return true;
        }

        isMoving = false;
        return false;
    }

    private bool isDirectionFree(Vector2 direction)
    {
        int collisionCount = rigidBody.Cast(direction, moveFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (collisionCount == 0)
        {
            return true;
        }

        return false;
    }

    private void UpdateMoveDegrees()
    {
        moveDegrees = AngleCalculation.getAngle(moveDirection, Vector2.zero);
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (moveDirection == Vector2.zero)
        {
            isMoving = false;
            return;
        }

        bool hasMoved = MovePlayer(moveDirection);

        // Try to slide along the collider (horizontally first) if we collide
        if (!hasMoved)
        {
            hasMoved = MovePlayer(new Vector2(moveDirection.x, 0));

            if (!hasMoved)
            {
                MovePlayer(new Vector2(0, moveDirection.y));
            }
        }

        UpdateMoveDegrees();
    }
}

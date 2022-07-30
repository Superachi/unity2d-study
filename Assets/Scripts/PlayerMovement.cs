using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    // Movement
    public Vector2 moveDirection;
    public float moveSpeed = 10f;
    public float moveDegrees;
    public bool isMoving;

    // Collision handling
    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isMoving = false;
        moveDegrees = 90;
    }

    private void GetInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        // Make it so we don't move faster diagonally
        moveDirection = moveDirection.normalized;
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

    private bool MovePlayer(Vector2 direction)
    {
        // We did not move if our directional Vector2 is (0, 0), so return false
        if (direction == Vector2.zero) return false;

        if (isDirectionFree(direction))
        {
            rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    private void UpdateMoveDegrees()
    {
        moveDegrees = AngleCalculation.getAngle(moveDirection, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (moveDirection == Vector2.zero) return;

        bool hasMoved = MovePlayer(moveDirection);

        // Try to slide along the collider (horizontally first) if we collide
        if (!hasMoved)
        {
            // Need to save the movement vector's Y here, as we overwrite this with 0 in the line after
            // Yet we need the Y to slide vertically along the walls
            float oldMoveY = moveDirection.y;
            moveDirection = new Vector2(moveDirection.x, 0);

            hasMoved = MovePlayer(moveDirection);
            if (!hasMoved)
            {
                moveDirection = new Vector2(0, oldMoveY);
                MovePlayer(moveDirection);
            }
        }

        UpdateMoveDegrees();
    }
}

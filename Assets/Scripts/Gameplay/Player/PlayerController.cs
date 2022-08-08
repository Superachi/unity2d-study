using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    public float anglePlayerToMouse;
    public float cardinalPlayerToMouse;
    public bool fireButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void GetInput()
    {
        Vector2 moveInput = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };

        // Make it so we don't move faster diagonally
        playerMovement.moveDirection = moveInput.normalized;

        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        anglePlayerToMouse = AngleCalc.AngleBetweenPoints(gameObject.transform.position, mouseWorldPos);
        cardinalPlayerToMouse = AngleCalc.AngleToCardinal(anglePlayerToMouse);

        if (Input.GetButton("Fire1")) {
            if (playerShoot.canShoot) playerShoot.Attack(gameObject.transform.position, mouseWorldPos, 0.3f);
            fireButtonPressed = true;
        } else
        {
            fireButtonPressed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
}

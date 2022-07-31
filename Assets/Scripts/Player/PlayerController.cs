using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;

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

        if (Input.GetButtonDown("Fire1")) {
            Vector2 screenPosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            playerShoot.CreateBullet(gameObject.transform.position, worldPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
}

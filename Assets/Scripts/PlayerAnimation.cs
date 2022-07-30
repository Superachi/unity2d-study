using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    public enum animationStates
    {
        PlayerIdleLeft = 0,
        PlayerIdleRight = 1,
        PlayerIdleUp = 2,
        PlayerIdleDown = 3,
        PlayerWalkLeft = 4,
        PlayerWalkRight = 5,
        PlayerWalkUp = 6,
        PlayerWalkDown = 7
    }

    private animationStates currentState;

    void ChangeAnimationState(animationStates newState)
    {
        // If already playing the animation, don't have it interrupt itself
        if (currentState == newState) return;

        animator.Play(newState.ToString());
        currentState = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        currentState = animationStates.PlayerIdleRight;
        animator.Play(currentState.ToString());
    }

    void AnimatePlayer()
    {
        bool playerMoving = playerMovement.moveDirection != Vector2.zero;
        switch (playerMovement.moveDegrees)
        {
            case AngleCalculation.ANGLE_UP:
                if (playerMoving) ChangeAnimationState(animationStates.PlayerWalkUp);
                else ChangeAnimationState(animationStates.PlayerIdleUp);
                break;
            case AngleCalculation.ANGLE_RIGHT:
            case AngleCalculation.ANGLE_UP_RIGHT:
            case AngleCalculation.ANGLE_DOWN_RIGHT:
                if (playerMoving) ChangeAnimationState(animationStates.PlayerWalkRight);
                else ChangeAnimationState(animationStates.PlayerIdleRight);
                break;
            case AngleCalculation.ANGLE_DOWN:
                if (playerMoving) ChangeAnimationState(animationStates.PlayerWalkDown);
                else ChangeAnimationState(animationStates.PlayerIdleDown);
                break;
            case AngleCalculation.ANGLE_LEFT:
            case AngleCalculation.ANGLE_UP_LEFT:
            case AngleCalculation.ANGLE_DOWN_LEFT:
                if (playerMoving) ChangeAnimationState(animationStates.PlayerWalkLeft);
                else ChangeAnimationState(animationStates.PlayerIdleLeft);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        AnimatePlayer();
    }
}

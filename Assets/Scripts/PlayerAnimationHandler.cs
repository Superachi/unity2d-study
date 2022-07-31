using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to determine what animation to play for a player character
public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AnimationHandler animationHandler;
    private Dictionary<string, Sprite[]> spriteDictionary = new Dictionary<string, Sprite[]>();
    public float animWalkSpeed = 4f;

    public Sprite[] spritesIdleSide;
    public Sprite[] spritesIdleUp;
    public Sprite[] spritesIdleDown;
    public Sprite[] spritesWalkSide;
    public Sprite[] spritesWalkUp;
    public Sprite[] spritesWalkDown;

    public enum animationStates
    {
        AnimIdleLeft = 0,
        AnimIdleRight = 1,
        AnimIdleUp = 2,
        AnimIdleDown = 3,
        AnimWalkLeft = 4,
        AnimWalkRight = 5,
        AnimWalkUp = 6,
        AnimWalkDown = 7
    }

    private animationStates currentState = animationStates.AnimIdleRight;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animationHandler = GetComponent<AnimationHandler>();
        SpriteDictionaryInit();
    }

    void SpriteDictionaryInit()
    {
        spriteDictionary.Add(animationStates.AnimIdleLeft.ToString(),   spritesIdleSide);
        spriteDictionary.Add(animationStates.AnimIdleRight.ToString(),  spritesIdleSide);
        spriteDictionary.Add(animationStates.AnimIdleUp.ToString(),     spritesIdleUp);
        spriteDictionary.Add(animationStates.AnimIdleDown.ToString(),   spritesIdleDown);
        spriteDictionary.Add(animationStates.AnimWalkLeft.ToString(),   spritesWalkSide);
        spriteDictionary.Add(animationStates.AnimWalkRight.ToString(),  spritesWalkSide);
        spriteDictionary.Add(animationStates.AnimWalkUp.ToString(),     spritesWalkUp);
        spriteDictionary.Add(animationStates.AnimWalkDown.ToString(),   spritesWalkDown);
    }

    void ChangeAnimationState(animationStates newState)
    {
        // If already playing the animation, don't have it interrupt itself
        if (currentState == newState) return;

        currentState = newState;
        bool flipX = currentState.ToString().Contains("Left");
        animationHandler.setAnimation(spriteDictionary[currentState.ToString()], 0, animWalkSpeed, true, flipX);
    }

    void AnimatePlayer()
    {
        switch (playerMovement.moveDegrees)
        {
            case AngleCalculation.ANGLE_UP:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkUp);
                else ChangeAnimationState(animationStates.AnimIdleUp);
                break;
            case AngleCalculation.ANGLE_RIGHT:
            case AngleCalculation.ANGLE_UP_RIGHT:
            case AngleCalculation.ANGLE_DOWN_RIGHT:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkRight);
                else ChangeAnimationState(animationStates.AnimIdleRight);
                break;
            case AngleCalculation.ANGLE_DOWN:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkDown);
                else ChangeAnimationState(animationStates.AnimIdleDown);
                break;
            case AngleCalculation.ANGLE_LEFT:
            case AngleCalculation.ANGLE_UP_LEFT:
            case AngleCalculation.ANGLE_DOWN_LEFT:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkLeft);
                else ChangeAnimationState(animationStates.AnimIdleLeft);
                break;
        }
    }

    private void FixedUpdate()
    {
        AnimatePlayer();
    }
}

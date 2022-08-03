using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to determine what animation to play for a player character
public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private AnimationHandler animationHandler;
    private Dictionary<animationStates, Sprite[]> spriteDictionary = new();
    private float facingDirection = AngleCalc.ANGLE_RIGHT;

    public float animWalkSpeed;
    public float animAttackSpeed;

    public enum animationStates
    {
        AnimIdleLeft = 0,
        AnimIdleRight = 1,
        AnimIdleUp = 2,
        AnimIdleDown = 3,
        AnimWalkLeft = 4,
        AnimWalkRight = 5,
        AnimWalkUp = 6,
        AnimWalkDown = 7,
        AnimAttackLeft = 8,
        AnimAttackRight = 9,
        AnimAttackUp = 10,
        AnimAttackDown = 11,
    }

    private animationStates currentState = animationStates.AnimIdleRight;

    public Sprite[] spritesIdleSide;
    public Sprite[] spritesIdleUp;
    public Sprite[] spritesIdleDown;
    public Sprite[] spritesWalkSide;
    public Sprite[] spritesWalkUp;
    public Sprite[] spritesWalkDown;
    public Sprite[] spritesAttackSide;
    public Sprite[] spritesAttackUp;
    public Sprite[] spritesAttackDown;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        animationHandler = GetComponent<AnimationHandler>();
        SpriteDictionaryInit();
    }

    void SpriteDictionaryInit()
    {
        spriteDictionary.Add(animationStates.AnimIdleLeft,   spritesIdleSide);
        spriteDictionary.Add(animationStates.AnimIdleRight,  spritesIdleSide);
        spriteDictionary.Add(animationStates.AnimIdleUp,     spritesIdleUp);
        spriteDictionary.Add(animationStates.AnimIdleDown,   spritesIdleDown);
        spriteDictionary.Add(animationStates.AnimWalkLeft,   spritesWalkSide);
        spriteDictionary.Add(animationStates.AnimWalkRight,  spritesWalkSide);
        spriteDictionary.Add(animationStates.AnimWalkUp,     spritesWalkUp);
        spriteDictionary.Add(animationStates.AnimWalkDown,   spritesWalkDown);
        spriteDictionary.Add(animationStates.AnimAttackLeft,    spritesAttackSide);
        spriteDictionary.Add(animationStates.AnimAttackRight,   spritesAttackSide);
        spriteDictionary.Add(animationStates.AnimAttackUp,      spritesAttackUp);
        spriteDictionary.Add(animationStates.AnimAttackDown,    spritesAttackDown);
    }

    void ChangeAnimationState(animationStates newState)
    {
        // If already playing the animation, don't have it interrupt itself
        if (currentState == newState) return;
        currentState = newState;
        SetAnimationParams();
    }

    void SetAnimationParams()
    {
        float animSpeed = animWalkSpeed;
        if (currentState.ToString().Contains("Attack"))
        {
            animSpeed = animAttackSpeed;
        }

        bool flipX = currentState.ToString().Contains("Left");
        animationHandler.setAnimation(spriteDictionary[currentState], 0, animSpeed, true, flipX);
    }

    void ControlAnimation()
    {
        if (playerShoot.isShooting) facingDirection = playerController.cardinalPlayerToMouse;
        else if (playerMovement.isMoving) facingDirection = playerMovement.moveDegrees;

        if (playerShoot.isShooting)
        {
            switch (facingDirection)
            {
                case AngleCalc.ANGLE_UP:
                    ChangeAnimationState(animationStates.AnimAttackUp);
                    break;
                case AngleCalc.ANGLE_RIGHT:
                    ChangeAnimationState(animationStates.AnimAttackRight);
                    break;
                case AngleCalc.ANGLE_DOWN:
                    ChangeAnimationState(animationStates.AnimAttackDown);
                    break;
                case AngleCalc.ANGLE_LEFT:
                    ChangeAnimationState(animationStates.AnimAttackLeft);
                    break;
            }

            return;
        }

        switch (facingDirection)
        {
            case AngleCalc.ANGLE_UP:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkUp);
                else ChangeAnimationState(animationStates.AnimIdleUp);
                break;
            case AngleCalc.ANGLE_RIGHT:
            case AngleCalc.ANGLE_UP_RIGHT:
            case AngleCalc.ANGLE_DOWN_RIGHT:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkRight);
                else ChangeAnimationState(animationStates.AnimIdleRight);
                break;
            case AngleCalc.ANGLE_DOWN:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkDown);
                else ChangeAnimationState(animationStates.AnimIdleDown);
                break;
            case AngleCalc.ANGLE_LEFT:
            case AngleCalc.ANGLE_UP_LEFT:
            case AngleCalc.ANGLE_DOWN_LEFT:
                if (playerMovement.isMoving) ChangeAnimationState(animationStates.AnimWalkLeft);
                else ChangeAnimationState(animationStates.AnimIdleLeft);
                break;
        }
    }

    private void Update()
    {
        ControlAnimation();
    }
}

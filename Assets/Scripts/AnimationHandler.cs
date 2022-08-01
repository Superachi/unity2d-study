using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to handle a play a single animation, looping if required
public class AnimationHandler : MonoBehaviour
{
    // These variables are public so they can be assigned via the Unity editor
    // If not using the editor, use setAnimation() instead
    public Sprite[] spriteArray;
    public bool animateOnStart = false;
    public float animSpeed;
    public bool animIsLooping;

    private int animFrame;
    private int animFrameCount;
    private float animTimeRemaining;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animateOnStart) setAnimation(spriteArray, 0, animSpeed, animIsLooping, false);
    }

    public void setAnimation(Sprite[] array, int startingFrame, float speed, bool loop, bool flipX)
    {
        spriteArray = array;
        animFrameCount = spriteArray.Length;
        animFrame = startingFrame;
        animSpeed = speed;
        animIsLooping = loop;
        animTimeRemaining = 1;

        spriteRenderer.sprite = spriteArray[animFrame];
        spriteRenderer.flipX = flipX;
    }

    private void ChangeFrame(int frame)
    {
        spriteRenderer.sprite = spriteArray[frame];
    }

    private void Animate()
    {
        // Nothing to animate if we only have one frame or speed == 0
        // or if we reached the end of the array with !animIsLooping
        if (animFrameCount == 1 || animSpeed == 0) return;
        if (animFrame == animFrameCount && !animIsLooping)
        {
            Sprite[] array = new Sprite[] { spriteRenderer.sprite };
            setAnimation(array, 0, 0, false, spriteRenderer.flipX);
        }

        animTimeRemaining -= Time.fixedDeltaTime * animSpeed;
        if (animTimeRemaining <= 0)
        {
            animTimeRemaining = 1;

            animFrame++;
            if (animFrame > animFrameCount - 1) animFrame = 0;
            ChangeFrame(animFrame);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Animate();
    }
}

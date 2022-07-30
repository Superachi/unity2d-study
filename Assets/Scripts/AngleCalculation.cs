using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculation : MonoBehaviour
{
    // Based on this diagram http://academic.brooklyn.cuny.edu/geology/grocha/mapsdistancelatlong/directionlatlong.htm
    public const float NO_ANGLE = -1;
    public const float ANGLE_UP = 0;
    public const float ANGLE_UP_RIGHT = 45;
    public const float ANGLE_RIGHT = 90;
    public const float ANGLE_DOWN_RIGHT = 135;
    public const float ANGLE_DOWN = 180;
    public const float ANGLE_DOWN_LEFT = 225;
    public const float ANGLE_LEFT = 270;
    public const float ANGLE_UP_LEFT = 315;

    public static float getAngle(Vector2 pointA, Vector2 pointB)
    {
        Vector2 delta = pointA - pointB;
        float angleRadians = Mathf.Atan2(delta.x, delta.y);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees < 0)
        {
            angleDegrees += 360;
        }

        return angleDegrees;
    }
}

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

    public static float GetAngle(Vector2 pointA, Vector2 pointB)
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

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalc : MonoBehaviour
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

    public static float AngleBetweenPoints(Vector2 pointFrom, Vector2 pointTo)
    {
        Vector2 delta = pointTo - pointFrom;
        float angleRadians = Mathf.Atan2(delta.x, delta.y);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees < 0)
        {
            angleDegrees += 360;
        }

        return angleDegrees;
    }

    public static float AngleToCardinal(float angle)
    {
        if (angle >= 45 && angle < 135) return ANGLE_RIGHT;
        if (angle >= 135 && angle < 225) return ANGLE_DOWN;
        if (angle >= 225 && angle < 315) return ANGLE_LEFT;
        return ANGLE_UP;
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static float Vector2ToDegrees(Vector2 vector)
    {
        return AngleBetweenPoints(Vector2.zero, vector);
    }

    public static float AngleCorrect(float angle)
    {
        return angle - 90;
    }

    public static Vector2 LengthDirection(float length, float direction, Vector2 startPoint = default)
    {
        Vector2 directionVector = DegreeToVector2(direction);
        return LengthDirection(length, directionVector, startPoint);
    }

    public static Vector2 LengthDirection(float length, Vector2 direction, Vector2 startPoint = default)
    {
        return startPoint + (direction.normalized * length);
    }
}

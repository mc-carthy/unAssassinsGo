using UnityEngine;

public class Utility
{
    public static Vector3 Vector3Round(Vector3 input)
    {
        return new Vector3(
            Mathf.Round(input.x),
            Mathf.Round(input.y),
            Mathf.Round(input.z)
        );
    }

    public static Vector2 Vector2Round(Vector2 input)
    {
        return new Vector2(
            Mathf.Round(input.x),
            Mathf.Round(input.y)
        );
    }
}

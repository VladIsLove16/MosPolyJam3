using UnityEngine;

public static class PointerInput
{
    public static Vector3 GetPointerInput()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector2 GetPointerInputVector2()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

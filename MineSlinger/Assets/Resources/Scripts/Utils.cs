using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector2 GetMousePosition()
    {
        return CameraController.Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}

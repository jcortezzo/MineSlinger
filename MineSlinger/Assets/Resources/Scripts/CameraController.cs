using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera Camera { get; private set; }

    private void Awake()
    {
        Camera = Camera.main;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraHelper01 : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private Camera _Camera;

    public Camera Camera
    {
        get => _Camera;
        private set => _Camera = value;
    }

    public void SetCameraClearFlagsNothing()
    {
        Camera.clearFlags = CameraClearFlags.Nothing;
    }

    public void SetCameraClearFlagsColor()
    {
        Camera.clearFlags = CameraClearFlags.Color;
    }

    public void OnBeforeSerialize()
    {
        if (Camera == null)
            Camera = GetComponent<Camera>();
    }
    public void OnAfterDeserialize()
    {
    }
}

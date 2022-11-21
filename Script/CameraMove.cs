using System;
using UnityEngine;

public class CameraMove
{
    Camera mainCamera;

    public CameraMove(Camera maincamera)
    {
        mainCamera = maincamera;
    }

    internal void CameraReset()
    {
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }

}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class CameraMove
{
    Camera mainCamera;
    private Vector3 CameraCurrent = new Vector3();
    private Vector3 CameraReset = new Vector3(0, 0, -10);

    private float shootTime = 0;

    public CameraMove(Camera maincamera)
    {
        mainCamera = maincamera;
    }

    internal void CameraInit()
    {
        mainCamera.transform.position = CameraReset;
    }

    internal IEnumerator MoveCameraCorutin()
    {
        CameraCurrent = mainCamera.transform.position;

        var t = 0f;

        while (t < 1)
        {
            if(CameraCurrent.x < CameraReset.x)
            {
                CameraCurrent = CameraReset;
                break;
            }

            t += Time.deltaTime / 1.5f;
            mainCamera.transform.position = Vector3.Lerp(CameraCurrent, CameraReset, t);
            mainCamera.orthographicSize = Mathf.Lerp(10f, 5f, t);
            shootTime = 0;
            yield return null;
        }

        yield return null;
    }

    internal void CameraChase(Vector3 ballmove)
    {
        Vector3 ballMoveControll = ballmove;
        ballMoveControll.z = -10;

        if (ballMoveControll.y < 0)
            ballMoveControll.y = 0;

        if (ballMoveControll.x > 30f)
        {
            ballMoveControll.x = 30f;
            mainCamera.transform.position = ballMoveControll;
        }
        else if(ballMoveControll.x > 0)
        {
            mainCamera.transform.position = ballMoveControll;
            CameraFall();
        }
    }

    private void CameraFall()
    {
        shootTime += Time.deltaTime / 1.5f;
        mainCamera.orthographicSize = Mathf.Lerp(5f, 10f, shootTime);
    }
}

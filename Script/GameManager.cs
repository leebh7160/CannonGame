using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera Camera_main;
    [SerializeField]
    private Transform Block_X;
    [SerializeField]
    private Transform Block_Y;
    [SerializeField]
    private Cannon cannon;


    private bool ballShootingCheck = false;

    private CameraMove cameraMove;
    protected CannonBall Ball_Shooting;

    void Start()
    {
        cannon.GameManager = this;
        Camera_main = Camera.main;
        cameraMove = new CameraMove(Camera_main);

        cameraMove.CameraReset();
    }

    private void ShootingBallObject(CannonBall ball)
    {
        Ball_Shooting = ball;
        ballShootingCheck = true;
    }

    private void ShootingBallDelete()
    {
        Ball_Shooting = null;
        ballShootingCheck = false;
    }


    void Update()
    {
        cannon.CannonMove();

        if(ballShootingCheck == true)
        {
        }
    }
}

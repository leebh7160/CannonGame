using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private CannonBall[] Cannon_Ball;
    [SerializeField]
    private Transform Cannon_Gun;
    [SerializeField]
    private Transform Cannon_Shoot;


    private GameManager gameManager;
    public GameManager GameManager
    { get => gameManager; set { gameManager = value; } }

    private Camera m_Cam;
    private bool isCannon_Shoot = false;
    private int Cannon_BallNumber = 5;
    private int Cannon_BallCount = 0;

    void Start()
    {
        m_Cam = Camera.main;
    }

    public void CannonMove()
    {
        if (isCannon_Shoot == false)
        {
            TryFire();
            LookAtMouse();
        }
    }


    private void TryFire()
    {
        if (Input.GetMouseButtonDown(0) && isCannon_Shoot == false && Cannon_BallCount < Cannon_BallNumber)
        {
            Cannon_Ball[Cannon_BallCount].Cannon = this.GetComponent<Cannon>();
            Cannon_Ball[Cannon_BallCount].transform.position = Cannon_Shoot.position;
            Cannon_Ball[Cannon_BallCount].transform.rotation = Cannon_Shoot.rotation;
            Cannon_Ball[Cannon_BallCount].GetComponent<Rigidbody2D>().velocity = Cannon_Ball[Cannon_BallCount].transform.up * 23f;
            Cannon_Ball[Cannon_BallCount].BallActive();

            Send_ShootingCannonBall(Cannon_Ball[Cannon_BallCount]);

            isCannon_Shoot = true;
            Cannon_BallCount += 1;
            Cannon_ShootChange();
        }
    }

    private void LookAtMouse()
    {
        Vector2 t_mousePos = m_Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 t_direction = new Vector2(t_mousePos.x - Cannon_Gun.position.x,
                                          t_mousePos.y - Cannon_Gun.position.y);

        if (t_direction.x < 0)
            t_direction.x = 0;
        if (t_direction.y < 0)
            t_direction.y = 0;

        Cannon_Gun.up = t_direction;
    }

    private void Cannon_ShootChange()
    {
        gameManager.CannonBallCount(Cannon_BallCount);
    }

    public void Cannon_Reset()
    {
        isCannon_Shoot = false;
        Cannon_BallCount = 0;
    }

    #region Send È®ÀÎ
    private void Send_ShootingCannonBall(CannonBall cannonball)
    {
        gameManager.SendMessage("ShootingBallObject", cannonball);
    }

    public void Send_BallDelete()
    {
        isCannon_Shoot = false;
        gameManager.SendMessage("ShootingBallDelete", Cannon_BallCount);
    }
    #endregion
}

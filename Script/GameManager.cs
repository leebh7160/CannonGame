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
    private Transform BlockSetting;
    [SerializeField]
    private Cannon cannon;
    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private Box scoreBox;

    private Transform EnemyBoxParent;

    private List<Transform> XblockDropList = new List<Transform>();
    private List<Transform> YblockDropList = new List<Transform>();

    private bool ballShootingCheck = false;
    private int TotalScore = 0;

    private CameraMove cameraMove;
    protected CannonBall Ball_Shooting;

    void Start()
    {
        cannon.GameManager = this;
        scoreBox.GameManager = this;

        EnemyBoxParent = GameObject.Find("EnemyBlock").transform;
        Camera_main = Camera.main;
        cameraMove = new CameraMove(Camera_main);

        BlockMaking();
        cameraMove.CameraInit();
    }

    void Update()
    {
        cannon.CannonMove();

        if (ballShootingCheck == true)
            cameraMove.CameraChase(Ball_Shooting.transform.position);
    }

    public void ResetGame()
    {
        ballShootingCheck = false;
        Ball_Shooting = null;
        TotalScore = 0;

        cannon.Cannon_Reset();
        cameraMove.CameraInit();
        ResetBlockMaking();
        gameUI.InitUI();
    }

    #region 블록 만들기 및 세팅
    private void BlockMaking()
    {
        Transform XBoxObject;
        Transform YBoxObject;

        float XPosition = -6f; //3씩 더하기
        //Y 값
        float XPosition_1F = -1.5f;//Y 값
        float XPosition_2F = 3f;

        float YPosition = -7f; //3.5씩 더하기
        //X 값
        float YPosition_1F = -3.5f; 
        float YPosition_2F = 0.5f; 

        Vector3 xPositionVector = new Vector3();
        Vector3 yPositionVector = new Vector3();

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                XPosition = -6f;
                YPosition = -7f;
            }

            if (i < 5)
            {
                xPositionVector = new Vector3(XPosition + 30, XPosition_1F);
                yPositionVector = new Vector3(YPosition + 30, YPosition_1F);
            }
            else
            {
                xPositionVector = new Vector3(XPosition + 30, XPosition_2F);
                yPositionVector = new Vector3(YPosition + 30, YPosition_2F);
            }

            XBoxObject = Instantiate(Block_X, xPositionVector, Quaternion.identity, EnemyBoxParent);
            YBoxObject = Instantiate(Block_Y, yPositionVector, Quaternion.identity, EnemyBoxParent);

            XBoxObject.name = "XBox" + i;
            YBoxObject.name = "YBox" + i;

            XblockDropList.Add(XBoxObject);
            YblockDropList.Add(YBoxObject);

            XBoxObject = null;
            YBoxObject = null;

            XPosition += 3;
            YPosition += 3.5f;
        }
    }

    private void ResetBlockMaking()
    {
        float XPosition = -6f; //3씩 더하기
        //Y 값
        float XPosition_1F = -1.5f;//Y 값
        float XPosition_2F = 3f;

        float YPosition = -7f; //3.5씩 더하기
        //X 값
        float YPosition_1F = -3.5f;
        float YPosition_2F = 0.5f;

        Vector3 xPositionVector = new Vector3();
        Vector3 yPositionVector = new Vector3();

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                XPosition = -6f;
                YPosition = -7f;
            }

            if (i < 5)
            {
                xPositionVector = new Vector3(XPosition + 30, XPosition_1F);
                yPositionVector = new Vector3(YPosition + 30, YPosition_1F);
            }
            else
            {
                xPositionVector = new Vector3(XPosition + 30, XPosition_2F);
                yPositionVector = new Vector3(YPosition + 30, YPosition_2F);
            }

            XblockDropList[i].position = xPositionVector;
            XblockDropList[i].eulerAngles = Vector3.zero;
            YblockDropList[i].position = yPositionVector;
            YblockDropList[i].eulerAngles = Vector3.zero;
            XblockDropList[i].gameObject.SetActive(true);
            YblockDropList[i].gameObject.SetActive(true);

            XPosition += 3;
            YPosition += 3.5f;
        }
    }
    #endregion

    #region cannon에서 send로 메시지 실행
    private void ShootingBallObject(CannonBall ball)
    {
        Ball_Shooting = ball;
        ballShootingCheck = true;
    }

    private void ShootingBallDelete(int ballcount)
    {
        Ball_Shooting = null;
        ballShootingCheck = false;
        StartCoroutine(cameraMove.MoveCameraCorutin());
        if (ballcount == 5)
        {
            gameUI.EndUI();
        }
    }
    #endregion

    internal void CannonBallCount(int ballcount)
    {
        gameUI.CannonballCount(ballcount);
    }

    #region 점수 체크
    internal void BoxDownCheck(string boxnamecheck, int boxlistnum)
    {
        TotalScore += 1;
        if (boxnamecheck == "X")
            XblockDropList[boxlistnum].gameObject.SetActive(false);
        if (boxnamecheck == "Y")
            YblockDropList[boxlistnum].gameObject.SetActive(false);

        gameUI.ScoreChange(TotalScore);
    }
    #endregion
}

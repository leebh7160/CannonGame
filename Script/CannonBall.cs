using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Cannon cannon;
    public Cannon Cannon
        { set { cannon = value; } get { return cannon; } }

    private Rigidbody2D ballRigid2D;
    private CircleCollider2D circleCollider;
    private TrailRenderer ballTrail;

    private bool isCollision = false;
    private float activeTime = 5;

    private void Start()
    {
        ballRigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        ballTrail   = this.gameObject.GetComponentInChildren<TrailRenderer>();
        circleCollider = this.gameObject.GetComponentInChildren<CircleCollider2D>();
        activeTime = 5;
    }

    private void Update()
    {
        //발사 후 부딫힌다음에 시간 세어주기
        BallErase();

        if (isCollision == true)
            DeleteTime();
    }

    private void DeleteTime()
    {
        activeTime -= Time.deltaTime * 1;
        Debug.Log(activeTime);
    }


    public void BallActive()
    {
        ballRigid2D.simulated = true;
        circleCollider.isTrigger = false;
        ballTrail.time = 0.3f;
    }

    private void BallErase()
    {
        if (this.gameObject.transform.position.y < -25f || activeTime < 0)
        {
            activeTime = 5;
            isCollision = false;
            ballTrail.time = 0.0f;
            ballRigid2D.simulated = false;
            circleCollider.isTrigger = true;
            this.gameObject.transform.position = new Vector3(-8, -3.8f, 0);
            cannon.Send_BallDelete();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollision = true;
    }

}

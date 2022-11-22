using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI cannonText;
    [SerializeField]
    private Button replayButton;

    internal void ScoreChange(float score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    internal void CannonballCount(int cannonNum)
    {
        cannonText.text = "CannonBall : " + cannonNum.ToString();
    }

    internal void EndUI()
    {
        replayButton.gameObject.SetActive(true);
    }

    internal void InitUI()
    {
        scoreText.text = "Score : 0";
        cannonText.text = "CannonBall : 0";
        replayButton.gameObject.SetActive(false);
    }
}

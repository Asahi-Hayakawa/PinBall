using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    // ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    // ゲームオーバを表示するテキスト
    private GameObject gameOverText;

    // 得点を表示するテキスト
    private GameObject scoreText;

    // 現在の得点
    private int currentScore;
    private int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            this.scoreText.GetComponent<Text>().text = "Score: " + currentScore;
        }
    } 

    // Start is called before the first frame update
    void Start()
    {
        // シーン中のGameOverTextオブジェクトを取得
        this.gameOverText = GameObject.Find("GameOverText");
        // シーン中のScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
        // 現在の得点を「0」に設定
        this.CurrentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            // GameOverTextにゲームオーバを表示
            this.gameOverText.GetComponent<Text>().text = "Game Over";
        }
    }

    // 衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        // Lesson5課題 UIのTextを使って得点を表示
        switch (other.collider.tag)
        {
            // 小さい星　得点+10
            case "SmallStarTag":
                this.CurrentScore += 10;
                break;
            // 大きい星　得点+30
            case "LargeStarTag":
                this.CurrentScore += 30;
                break;
            // 小さい雲　得点+20
            case "SmallCloudTag":
                this.CurrentScore += 20;
                break;
            // 大きい雲　得点+40
            case "LargeCloudTag":
                this.CurrentScore += 40;
                break;
        }
    }
}

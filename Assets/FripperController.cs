using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    // HingeJointコンポーネント
    private HingeJoint myHingeJoint;
    
    // 初期の傾き
    private float defaultAngle = 20;

    // 弾いた時の傾き
    private float flickAngle = -20;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        // フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        // Lesson 5発展課題
        // キーボード操作
        // 「←」、「A」押下： 左フリッパーを動かす
        // 「→」、「D」押下： 右フリッパーを動かす
        // 「↓」、「S」押下： 左右フリッパーを同時に動かす
        if (((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && tag == "LeftFripperTag") ||
            ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && tag == "RightFripperTag") ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetAngle(this.flickAngle);
        }

        // 「←」、「A」離した時： 左フリッパーを元に戻す
        // 「→」、「D」離した時： 右フリッパーを元に戻す
        // 「↓」、「S」離した時： 左右フリッパーを元に戻す
        if (((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && tag == "LeftFripperTag") ||
            ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && tag == "RightFripperTag") ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.DownArrow))
        {
            SetAngle(this.defaultAngle);
        }

        // タッチ操作
        if (Input.touches != null)
        {
            foreach (var touch in Input.touches)
            {
                Vector2 touchPos = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    // 画面の真ん中より左側でタップした時は左フリッパーを動かしてください
                    if (touchPos.x < Screen.width / 2 && tag == "LeftFripperTag")
                    {
                        SetAngle(this.flickAngle);
                    }
                    // 画面の真ん中より右側でタップした時は右フリッパーを動かしてください
                    if (touchPos.x > Screen.width / 2 && tag == "RightFripperTag")
                    {
                        SetAngle(this.flickAngle);
                    }                  
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // タップが終わった時にフリッパーを元の位置に戻してください
                    if (touchPos.x < Screen.width / 2 && tag == "LeftFripperTag")
                    {
                        SetAngle(this.defaultAngle);
                    }
                    if (touchPos.x > Screen.width / 2 && tag == "RightFripperTag")
                    {
                        SetAngle(this.defaultAngle);
                    }
                }
            }
        }
    }

    // フリッパーの傾きを設定
    private void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}

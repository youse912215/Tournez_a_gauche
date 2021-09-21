/* 主にカメラの操作 */

using System;
using PLAYER;
using UnityEngine;
using static Call.ConstantValue;

public class PlayerController : MonoBehaviour
{
    //回転向き
    public enum DIRECTION
    {
        NONE, //無回転 
        LEFT, //左回転
        RIGHT //右回転
    }

    public static float angle; //角度
    public static float rotate; //回転
    public static bool isInverse; //反転フラグ
    public static bool isChange; //切替フラグ
    public static bool isStop; //停止フラグ
    public static int color; //色
    private int isRotate; //回転フラグ

    // Start is called before the first frame update
    private void Start()
    {
        //初期化
        angle = 0.0f; //カメラ回転角度0
        isRotate = (int) DIRECTION.NONE; //無回転状態
        isInverse = false; //非反転状態
        isStop = false; //停止
        isChange = false; //切替なし
        transform.position = CF_POS; //カメラを初期位置にセット
        color = (int) WALL_COLOR.PINK;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("色:" + color);

        //色反転
        if (Input.GetKeyDown(KeyCode.R) && !cubeRotate.isWall)
        {
            //壁状態が青のとき
            if (color == (int) WALL_COLOR.BLUE)
                color = (int) WALL_COLOR.PINK; //ピンクに変更
            //壁状態がピンクのとき
            else if (color == (int) WALL_COLOR.PINK)
                color = (int) WALL_COLOR.BLUE; //青に変更
        }

        //左右反転
        if (!isStop && Input.GetKeyDown(KeyCode.E /*Joystick1Button1*/))
        {
            if (!isInverse)
            {
                //反転状態
                transform.position = CB_POS; //裏面にセット
                isChange = true; //切替フラグ開始
                isStop = true; //停止フラグ開始
                isInverse = true; //反転（表面→裏面）
            }
            else //非反転状態
            {
                transform.position = CF_POS; //表面にセット
                isChange = true; //切替フラグ開始
                isStop = true; //停止フラグ開始
                isInverse = false; //反転（裏面→表面）
            }
        }

        if (rotate == 0.0f && Input.GetKey(KeyCode.Q /*Joystick1Button4*/))
            isRotate = (int) DIRECTION.LEFT; //左回転
        else if (rotate == 0.0f && Input.GetKey(KeyCode.W /*Joystick1Button5*/))
            isRotate = (int) DIRECTION.RIGHT; //右回転

        if (Math.Abs(angle) == ONE_CIRCLE) angle = 0.0f; //一周したら、回転角度をリセット

        //左回転のとき
        if (isRotate == (int) DIRECTION.LEFT)
        {
            //角度によって分岐
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, -ROTATE_QUANTITY); //回転する
                rotate += ROTATE_QUANTITY; //回転量を与える
            }
            else if (rotate >= ONE_QUARTER)
            {
                isRotate = (int) DIRECTION.NONE; //無回転状態に戻す
                angle += ONE_QUARTER; //角度を90度
                rotate = 0.0f; //回転終了
            }
        }
        //右回転のとき
        else if (isRotate == (int) DIRECTION.RIGHT)
        {
            //角度によって分岐
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, ROTATE_QUANTITY); //回転する
                rotate += ROTATE_QUANTITY; //回転量を与える
            }
            else if (rotate >= ONE_QUARTER)
            {
                isRotate = (int) DIRECTION.NONE; //無回転状態に戻す
                angle -= ONE_QUARTER; //角度を-90度
                rotate = 0.0f; //回転終了
            }
        }
    }
}
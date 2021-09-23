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
    public static uint isFlag;
    public static bool isReset;
    public static bool isMenu;
    private AudioSource audioSource;
    private int isRotate; //回転フラグ
    private float menuCount;

    // 効果音用変数
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize(); //初期化
        audioSource = GetComponent<AudioSource>(); //Componentを取得
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("全フラグ" + isFlag);
        //Debug.Log("色" + color);
        Debug.Log("カウント" + menuCount);

        AdditionCount(); //メニューカウント加算

        if (!isMenu && Input.GetKey(KeyCode.Return)) isFlag = (uint) FLAG_KEY.NONE; //メニューフラグ終了

        if (isFlag == (uint) FLAG_KEY.MENU) return; //メニューフラグのとき、他操作はスキップ

        SubtractCount(); //メニューカウント減算

        //メニュー
        if (menuCount == 0.0f && isFlag == (uint) FLAG_KEY.NONE && Input.GetKey(KeyCode.Return))
            isFlag = (uint) FLAG_KEY.MENU; //メニューフラグ開始

        //リセット
        if (isFlag == (uint) FLAG_KEY.NONE && Input.GetKey(KeyCode.Space))
        {
            isReset = true;
            isFlag = (uint) FLAG_KEY.RESET;
            transform.Rotate(0.0f, 0.0f, angle);
            color = (int) WALL_COLOR.PINK;
            angle = 0.0f; //カメラ回転角度0
            rotate = 0.0f; //回転角0
            isRotate = (int) DIRECTION.NONE; //無回転状態
            isInverse = false; //非反転状態
            color = (int) WALL_COLOR.PINK;
            menuCount = 0.0f;
        }

        //色反転
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.W) && !cubeRotate.isWall)
        {
            audioSource.PlayOneShot(sound1); // 効果音を鳴らす
            //壁状態が青のとき
            if (color == (int) WALL_COLOR.BLUE)
            {
                color = (int) WALL_COLOR.PINK; //ピンクに変更
                isStop = true; //停止フラグ開始
                isFlag = (uint) FLAG_KEY.C_CHANGE; //0100
            }
            //壁状態がピンクのとき
            else if (color == (int) WALL_COLOR.PINK)
            {
                color = (int) WALL_COLOR.BLUE; //青に変更
                isStop = true; //停止フラグ開始
                isFlag = (uint) FLAG_KEY.C_CHANGE; //0100
            }
        }

        //左右反転
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.Q /*Joystick1Button1*/))
        {
            audioSource.PlayOneShot(sound2); // 効果音を鳴らす
            if (!isInverse)
            {
                //反転状態
                transform.position = CB_POS; //裏面にセット
                isChange = true; //切替フラグ開始
                isStop = true; //停止フラグ開始
                isInverse = true; //反転（表面→裏面）
                isFlag = (uint) FLAG_KEY.INVERSE; //0010
            }
            else //非反転状態
            {
                transform.position = CF_POS; //表面にセット
                isChange = true; //切替フラグ開始
                isStop = true; //停止フラグ開始
                isInverse = false; //反転（裏面→表面）
                isFlag = (uint) FLAG_KEY.INVERSE; //0010
            }
        }

        //正転
        if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f && Input.GetKey(KeyCode.LeftArrow /*Joystick1Button4*/))
        {
            audioSource.PlayOneShot(sound3); // 効果音を鳴らす
            isRotate = (int) DIRECTION.LEFT; //左回転
            isFlag = (uint) FLAG_KEY.ROTATE; //0001
        }
        //逆転
        else if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f &&
                 Input.GetKey(KeyCode.RightArrow /*Joystick1Button5*/))
        {
            audioSource.PlayOneShot(sound3); // 効果音を鳴らす
            isRotate = (int) DIRECTION.RIGHT; //右回転
            isFlag = (uint) FLAG_KEY.ROTATE; //0001
        }

        if (Math.Abs(angle) == ONE_CIRCLE) angle = 0.0f; //一周したら、回転角度をリセット

        LeftRotate(); //左回転
        RightRotate(); //右回転
    }

    private void LeftRotate()
    {
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
                isFlag = (uint) FLAG_KEY.NONE; //フラグ関連をリセット
            }
        }
    }

    private void RightRotate()
    {
        //右回転のとき
        if (isRotate == (int) DIRECTION.RIGHT)
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
                isFlag = (uint) FLAG_KEY.NONE; //フラグ関連をリセット
            }
        }
    }

    private void AdditionCount()
    {
        if (isFlag == (uint) FLAG_KEY.MENU)
        {
            menuCount += LATE_SPEED;
            if (menuCount >= MENU_TIME)
            {
                isMenu = false;
                menuCount = MENU_TIME;
            }
        }
    }

    private void SubtractCount()
    {
        if (menuCount < 0.0f) menuCount = 0.0f;
        else menuCount -= LATE_SPEED;
    }

    //初期化
    private void Initialize()
    {
        angle = 0.0f; //カメラ回転角度0
        rotate = 0.0f; //回転角0
        isRotate = (int) DIRECTION.NONE; //無回転状態
        isInverse = false; //非反転状態
        isStop = false; //停止
        isChange = false; //切替なし
        color = (int) WALL_COLOR.PINK;
        isFlag = 0b0000; //0000にセット
        isReset = false;
        isMenu = false;
        menuCount = 0.0f;
    }
}
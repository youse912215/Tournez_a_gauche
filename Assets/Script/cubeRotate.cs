using System;
using System.Collections;
using UnityEngine;
using static Call.ConstantValue;

namespace PLAYER
{
    public class cubeRotate : MonoBehaviour
    {
        public static bool isWall; //壁フラグ
        public static bool isGoal; //ゴールフラグ
        public static uint isResult; //結果フラグ
        private float angleNum; //格納用の角度

        private Vector3 contactPoints; //衝突位置

        private float cubeAngle; //角度
        private float cubeSizeHalf; //キューブの大きさの半分
        private float currentAngel; //現在の角度

        private int currentWall; //現在の壁状態
        private float fallCount; //落下時間
        private bool isCollision; //衝突フラグ
        private bool isFloor; //床フラグ
        private bool isRotate; //回転フラグ

        private Vector3 rotateAxis = Vector3.zero; //軸
        private Vector3 rotatePoint = Vector3.zero; //中心
        private float stopCount; //停止時間
        private Vector3 stopPos; //停止位置
        private float widthB; //裏面の横幅
        private float widthF; //前面の横幅

        //初期位置セット
        private void InitSet(Vector3 pos, Vector3 rot, Vector3 scl)
        {
            transform.position = pos; //位置
            transform.rotation = Quaternion.Euler(rot); //回転角
            transform.localScale = scl; //規模
        }

        // Start is called before the first frame update
        private void Start()
        {
            //初期化
            InitSet(P_POS, P_ROT, P_SCL); //初期位置セット
            cubeSizeHalf = transform.localScale.x / 2.0f; //半キューブ
            cubeAngle = 0.0f; //キューブの角度
            currentAngel = 0.0f; //現在（カメラの）の回転角度
            stopCount = 0.0f; //停止時間
            isRotate = false; //回転フラグ
            isCollision = false; //衝突フラグ
            currentWall = (int) WALL_COLOR.PINK; //現在の壁状態をピンクに
            isWall = false; //壁フラグ
            isFloor = true; //床フラグ
            fallCount = 0.0f; //落下時間
            isGoal = false; //ゴールフラグ
            isResult = 0b0000; //000
        }

        // Update is called once per frame
        private void Update()
        {
            Debug.Log("床判定:" + isFloor);

            currentWall = PlayerController.color; //現在の色を取得

            if (!isFloor) transform.position -= FALL_SPEED; //床フラグがないとき、落下

            //回転角度と現在の回転角度が相違 or 切替フラグがtrueのとき
            if (angleNum != currentAngel || PlayerController.isChange)
                isCollision = false;

            //切替フラグがtrue and 反転フラグがtrueのとき
            if (PlayerController.isChange && PlayerController.isInverse)
            {
                SetInversePosition(); //現在位置を反転位置に設置
                PlayerController.isChange = false; //切替フラグ終了
            }
            else if (PlayerController.isChange && !PlayerController.isInverse)
            {
                SetForwardRotatePosition(); //現在位置を正転位置に設置
                PlayerController.isChange = false; //切替フラグ終了
            }

            GetResult(); //結果を取得

            InverseStop(); //反転による停止

            CalcAngel(); //角度を計算

            if (isCollision) FixFloorPosition(); //プレイヤー床の位置を修正

            if (PlayerController.isStop || !isFloor) return; //停止フラグがtrue or 床フラグがfalseのときスキップ

            //衝突していないとき
            if (!PlayerController.isStop && !isCollision && PlayerController.rotate == 0.0f)
            {
                if (isRotate) return; //回転フラグがtrueのときスキップ

                if (PlayerController.angle == 0.0f) LeftMove(); //左に移動
                else if (PlayerController.angle == 90.0f || PlayerController.angle == -270.0f) UpMove(); //上
                else if (PlayerController.angle == 180.0f || PlayerController.angle == -180.0f) RightMove(); //右
                else if (PlayerController.angle == 270.0f || PlayerController.angle == -90.0f) DownMove(); //下
                StartCoroutine(MoveCube()); //回転処理
                currentAngel = angleNum; //現在の回転角度を保存
            }
        }

        //衝突したとき
        private void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //衝突位置を取得

            if (collision.gameObject.tag != "wall") return; //タグがwallのとき

            //壁が現在の色以外のとき
            if (collision.gameObject.layer != currentWall)
            {
                isCollision = true; //衝突フラグ開始
                transform.position +=
                    new Vector3(
                        transform.position.x - contactPoints.x,
                        0.0f,
                        transform.position.z - contactPoints.z); //壁めり込み防止処理
                GetStopPosition(); //停止位置取得
            }
        }

        //衝突しているとき
        private void OnCollisionStay(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //衝突位置を取得

            //if (collision.gameObject.tag == "floor")
            //{
            //    isFloor = true;
            //}
            //else
            if (collision.gameObject.tag == "fall")
            {
                fallCount += LATE_SPEED; //待機速度を加算
                if (fallCount >= LATENCY) isFloor = false; //落下時間が待機時間を経過したとき、床フラグをfalse
            }

            if (collision.gameObject.tag == "goal")
            {
                isResult = 0b0001;
                isGoal = true;
            }

            if (collision.gameObject.tag != "wall") return; //壁以外のときスキップ

            //壁が現在の色以外のとき
            if (collision.gameObject.layer != currentWall)
            {
                isCollision = true; //衝突フラグ開始
                transform.position +=
                    new Vector3(
                        transform.position.x - contactPoints.x,
                        0.0f,
                        transform.position.z - contactPoints.z); //壁めり込み防止処理
                GetStopPosition(); //停止位置取得
            }

            isWall = true; //壁フラグ開始
        }

        //通過を終えたとき
        private void OnCollisionExit(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //衝突位置を取得

            if (collision.gameObject.tag == "fall") isFloor = false;

            if (collision.gameObject.tag != "wall") return; //壁以外のときスキップ

            isWall = false; //壁フラグ終了
        }

        //床の位置を修正
        private void FixFloorPosition()
        {
            SetStopPosition(); //停止位置に設置
            Debug.Log("衝突：" + isCollision); //衝突
        }

        //左移動
        private void LeftMove()
        {
            rotatePoint = transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0.0f);
            rotateAxis = new Vector3(0.0f, 0.0f, 1.0f);
        }

        //右移動
        private void RightMove()
        {
            rotatePoint = transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0.0f);
            rotateAxis = new Vector3(0.0f, 0.0f, -1.0f);
        }

        //上移動
        private void UpMove()
        {
            rotatePoint = transform.position + new Vector3(0.0f, -cubeSizeHalf, cubeSizeHalf);
            rotateAxis = new Vector3(1.0f, 0.0f, 0.0f);
        }

        //下移動
        private void DownMove()
        {
            rotatePoint = transform.position + new Vector3(0.0f, -cubeSizeHalf, -cubeSizeHalf);
            rotateAxis = new Vector3(-1.0f, 0.0f, 0.0f);
        }

        //角度を計算
        private void CalcAngel()
        {
            angleNum = PlayerController.angle; //カメラの角度を格納
        }

        private IEnumerator MoveCube()
        {
            isRotate = true; //回転開始

            //回転処理
            var sumAngle = 0.0f;
            while (sumAngle < ONE_QUARTER)
            {
                cubeAngle = ROTATE_SPEED; //回転する角度を代入
                sumAngle += cubeAngle; //回転速度を加算

                if (sumAngle > ONE_QUARTER) cubeAngle -= sumAngle - ONE_QUARTER; //過剰に回転しないよう制御
                transform.RotateAround(rotatePoint, rotateAxis, cubeAngle); //axisを中心として回転

                yield return null; //一時的に停止
            }

            isRotate = false; //回転終了
        }

        //現在位置を反転位置に設置
        private void SetInversePosition()
        {
            widthF = 2.0f * Math.Abs(transform.position.x - 240.0f) + SPACE; //現在位置から反転位置までの横幅を取得
            transform.position = new Vector3(
                transform.position.x + widthF,
                10.0f,
                transform.position.z); //取得した位置に設置
            GetStopPosition(); //停止位置取得
        }

        //現在位置を正転位置に設置
        private void SetForwardRotatePosition()
        {
            widthB = 2.0f * Math.Abs(transform.position.x - 720.0f) + SPACE; //現在位置から反転位置までの横幅を取得
            transform.position = new Vector3(
                transform.position.x - widthB,
                10.0f,
                transform.position.z); //取得した位置に設置
            GetStopPosition(); //停止位置取得
        }

        //反転による停止
        private void InverseStop()
        {
            //停止フラグがtrueのとき
            if (PlayerController.isStop)
            {
                stopCount += 0.1f; //停止時間起動
                if (PlayerController.isFlag == (uint) FLAG_KEY.INVERSE) SetStopPosition(); //停止位置に設置
            }

            //カウントがSTOP_TIME以上のとき
            if (stopCount >= STOP_TIME)
            {
                stopCount = 0.0f; //停止時間停止
                PlayerController.isStop = false; //停止フラグ終了
                PlayerController.isFlag = (uint) FLAG_KEY.NONE; //フラグ関連をリセット
            }
        }

        //停止位置取得
        private void GetStopPosition()
        {
            stopPos = new Vector3(transform.position.x, INITIAL_Y, transform.position.z); //位置取得
        }

        //停止位置に設置
        private void SetStopPosition()
        {
            transform.position = new Vector3(stopPos.x, INITIAL_Y, stopPos.z); //現在位置に停止位置を
            transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //回転角をリセット
        }

        private void GetResult()
        {
            if (transform.position.y <= -300.0f)
            {
                isResult = 0b0010;
                isGoal = true;
            }
        }

        //例外処理
        private void ExceptionHandling()
        {
            if (transform.position.y <= 0.0f && isFloor) SetStopPosition();
        }
    }
}
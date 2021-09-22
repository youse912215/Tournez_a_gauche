using UnityEngine;

/*呼び出し用スクリプト*/
namespace Call
{
    //定数値
    public class ConstantValue : MonoBehaviour
    {
        public enum FLAG_KEY : uint
        {
            //なし
            NONE = 0b0000,

            //色反転
            C_CHANGE = 0b0100,

            //左右反転
            INVERSE = 0b0010,

            //回転
            ROTATE = 0b0001,

            ROTATE2 = 0b1000
        }

        //シーン番号
        public enum SCENE_NAME
        {
            TITLE,
            SUNNY,
            CLOUDY,
            RAINY,
            RESULT
        }

        //壁の色番号
        public enum WALL_COLOR
        {
            PINK = 11,
            BLUE = 12
        }

        //計算用
        public const float ONE_QUARTER = 90.0f; //90度
        public const float ONE_CIRCLE = 360.0f; //360度
        public const float ROTATE_SPEED = 15.0f; //回転する速度
        public const float INTERVAL = 30.0f; //間隔
        public const float ROTATE_QUANTITY = 0.5f; //回転の変化量
        public const float STOP_TIME = 5.0f; //反転時の停止時間
        public const float LATE_SPEED = 0.1f; //待機速度
        public const float LATENCY = 1.0f; //待機時間
        public const int LEFTOVER = 2; //残り手数

        public const float SPACE = 480.0f; //マップ間のスペース
        public const float INITIAL_Y = 10.0f; //初期Y地点
        public static readonly Vector3 FALL_SPEED = new Vector3(0.0f, 5.0f, 0.0f); //落下速度

        //プレイヤー
        public static readonly Vector3 P_POS = new Vector3(215.0f, 10.0f, -55.0f); //位置
        public static readonly Vector3 P_ROT = new Vector3(0.0f, 0.0f, 0.0f); //角度
        public static readonly Vector3 P_SCL = new Vector3(5.0f, 5.0f, 5.0f); //規模

        public static readonly Quaternion P_CLEAR = new Quaternion(180.0f, 180.0f, 0.0f, 0.0f); //クリア時の回転角
        public static readonly Quaternion P_OVER = new Quaternion(-270.0f, 90.0f, 0.0f, 0.0f); //ゲームオーバー時の回転角

        public static readonly Color PINK_COLOR = new Color(1.0f, 0.665f, 0.959f, 1.0f); //ピンク
        public static readonly Color BLUE_COLOR = new Color(0.6f, 0.825f, 0.959f, 1.0f); //青

        //ステージ
        public static readonly Vector2 LEN = new Vector2(180.0f, 180.0f); //ステージ長さ

        //カメラ
        public static readonly Vector3 CF_POS = new Vector3(220.0f, 400.0f, -55.0f); //前位置
        public static readonly Vector3 CB_POS = new Vector3(885.0f, 400.0f, 10.0f); //後位置
        public static readonly Vector3 C_ROT = new Vector3(90.0f, 0.0f, 0.0f); //角度
        public static readonly Vector3 C_SCL = new Vector3(1.0f, 1.0f, 1.0f); //規模
    }
}
using UnityEngine;


/*呼び出し用スクリプト*/
namespace Call
{
    //定数値
    public class ConstantValue : MonoBehaviour
    {
        //計算用
        public const float ONE_QUARTER = 90.0f; //90度
        public const float ONE_CIRCLE = 360.0f; //360度
        public const float SPEED = 15.0f; //回転する速度
        public const float INTERVAL = 30.0f; //間隔
        public const float ROTATE_QUANTITY = 0.5f; //回転の変化量
        public const float STOP_TIME = 5.0f; //反転時の停止時間

        public const float SPACE = 480.0f; //マップ間のスペース
        public const float INITIAL_Y = 10.0f; //初期Y地点

        //プレイヤー
        public static readonly Vector3 P_POS = new Vector3(215.0f, 10.0f, -55.0f); //位置
        public static readonly Vector3 P_ROT = new Vector3(0.0f, 0.0f, 0.0f); //角度
        public static readonly Vector3 P_SCL = new Vector3(5.0f, 5.0f, 5.0f); //規模

        public static readonly Vector3 CENTER = new Vector3(145.0f, 10.0f, 10.0f); //中央

        //ステージ
        public static readonly Vector2 LEN = new Vector2(180.0f, 180.0f); //ステージ長さ

        //カメラ
        public static readonly Vector3 CF_POS = new Vector3(150.0f, 400.0f, 10.0f); //前位置
        public static readonly Vector3 CB_POS = new Vector3(815.0f, 400.0f, 10.0f); //後位置
        public static readonly Vector3 C_ROT = new Vector3(90.0f, 0.0f, 0.0f); //角度
        public static readonly Vector3 C_SCL = new Vector3(1.0f, 1.0f, 1.0f); //規模
    }
}
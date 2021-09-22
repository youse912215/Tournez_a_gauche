using UnityEngine;

/*�Ăяo���p�X�N���v�g*/
namespace Call
{
    //�萔�l
    public class ConstantValue : MonoBehaviour
    {
        public enum FLAG_KEY : uint
        {
            //�Ȃ�
            NONE = 0b0000,

            //�F���]
            C_CHANGE = 0b0100,

            //���E���]
            INVERSE = 0b0010,

            //��]
            ROTATE = 0b0001,

            ROTATE2 = 0b1000
        }

        //�V�[���ԍ�
        public enum SCENE_NAME
        {
            TITLE,
            SUNNY,
            CLOUDY,
            RAINY,
            RESULT
        }

        //�ǂ̐F�ԍ�
        public enum WALL_COLOR
        {
            PINK = 11,
            BLUE = 12
        }

        //�v�Z�p
        public const float ONE_QUARTER = 90.0f; //90�x
        public const float ONE_CIRCLE = 360.0f; //360�x
        public const float ROTATE_SPEED = 15.0f; //��]���鑬�x
        public const float INTERVAL = 30.0f; //�Ԋu
        public const float ROTATE_QUANTITY = 0.5f; //��]�̕ω���
        public const float STOP_TIME = 5.0f; //���]���̒�~����
        public const float LATE_SPEED = 0.1f; //�ҋ@���x
        public const float LATENCY = 1.0f; //�ҋ@����
        public const int LEFTOVER = 2; //�c��萔

        public const float SPACE = 480.0f; //�}�b�v�Ԃ̃X�y�[�X
        public const float INITIAL_Y = 10.0f; //����Y�n�_
        public static readonly Vector3 FALL_SPEED = new Vector3(0.0f, 5.0f, 0.0f); //�������x

        //�v���C���[
        public static readonly Vector3 P_POS = new Vector3(215.0f, 10.0f, -55.0f); //�ʒu
        public static readonly Vector3 P_ROT = new Vector3(0.0f, 0.0f, 0.0f); //�p�x
        public static readonly Vector3 P_SCL = new Vector3(5.0f, 5.0f, 5.0f); //�K��

        public static readonly Quaternion P_CLEAR = new Quaternion(180.0f, 180.0f, 0.0f, 0.0f); //�N���A���̉�]�p
        public static readonly Quaternion P_OVER = new Quaternion(-270.0f, 90.0f, 0.0f, 0.0f); //�Q�[���I�[�o�[���̉�]�p

        public static readonly Color PINK_COLOR = new Color(1.0f, 0.665f, 0.959f, 1.0f); //�s���N
        public static readonly Color BLUE_COLOR = new Color(0.6f, 0.825f, 0.959f, 1.0f); //��

        //�X�e�[�W
        public static readonly Vector2 LEN = new Vector2(180.0f, 180.0f); //�X�e�[�W����

        //�J����
        public static readonly Vector3 CF_POS = new Vector3(220.0f, 400.0f, -55.0f); //�O�ʒu
        public static readonly Vector3 CB_POS = new Vector3(885.0f, 400.0f, 10.0f); //��ʒu
        public static readonly Vector3 C_ROT = new Vector3(90.0f, 0.0f, 0.0f); //�p�x
        public static readonly Vector3 C_SCL = new Vector3(1.0f, 1.0f, 1.0f); //�K��
    }
}
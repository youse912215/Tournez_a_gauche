using UnityEngine;


/*�Ăяo���p�X�N���v�g*/
namespace Call
{
    //�萔�l
    public class ConstantValue : MonoBehaviour
    {
        //�v�Z�p
        public const float ONE_QUARTER = 90.0f; //90�x
        public const float ONE_CIRCLE = 360.0f; //360�x
        public const float SPEED = 15.0f; //��]���鑬�x
        public const float INTERVAL = 30.0f; //�Ԋu
        public const float ROTATE_QUANTITY = 0.5f; //��]�̕ω���
        public const float STOP_TIME = 5.0f; //���]���̒�~����

        public const float SPACE = 480.0f; //�}�b�v�Ԃ̃X�y�[�X
        public const float INITIAL_Y = 10.0f; //����Y�n�_

        //�v���C���[
        public static readonly Vector3 P_POS = new Vector3(215.0f, 10.0f, -55.0f); //�ʒu
        public static readonly Vector3 P_ROT = new Vector3(0.0f, 0.0f, 0.0f); //�p�x
        public static readonly Vector3 P_SCL = new Vector3(5.0f, 5.0f, 5.0f); //�K��

        public static readonly Vector3 CENTER = new Vector3(145.0f, 10.0f, 10.0f); //����

        //�X�e�[�W
        public static readonly Vector2 LEN = new Vector2(180.0f, 180.0f); //�X�e�[�W����

        //�J����
        public static readonly Vector3 CF_POS = new Vector3(150.0f, 400.0f, 10.0f); //�O�ʒu
        public static readonly Vector3 CB_POS = new Vector3(815.0f, 400.0f, 10.0f); //��ʒu
        public static readonly Vector3 C_ROT = new Vector3(90.0f, 0.0f, 0.0f); //�p�x
        public static readonly Vector3 C_SCL = new Vector3(1.0f, 1.0f, 1.0f); //�K��
    }
}
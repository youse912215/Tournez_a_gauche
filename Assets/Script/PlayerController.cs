/* ��ɃJ�����̑��� */

using System;
using PLAYER;
using UnityEngine;
using static Call.ConstantValue;

public class PlayerController : MonoBehaviour
{
    //��]����
    public enum DIRECTION
    {
        NONE, //����] 
        LEFT, //����]
        RIGHT //�E��]
    }

    public static float angle; //�p�x
    public static float rotate; //��]
    public static bool isInverse; //���]�t���O
    public static bool isChange; //�ؑփt���O
    public static bool isStop; //��~�t���O
    public static int color; //�F
    public static uint isFlag;
    private int isRotate; //��]�t���O

    // Start is called before the first frame update
    private void Start()
    {
        //������
        angle = 0.0f; //�J������]�p�x0
        isRotate = (int) DIRECTION.NONE; //����]���
        isInverse = false; //�񔽓]���
        isStop = false; //��~
        isChange = false; //�ؑւȂ�
        color = (int) WALL_COLOR.PINK;
        isFlag = 0b0000; //0000�ɃZ�b�g
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("�F:" + color);
        //Debug.Log("isFLAG:" + Convert.ToString(isFlag, 2));
        //Debug.Log("isSTOP:" + isStop);

        //�F���]
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.R) && !cubeRotate.isWall)
        {
            //�Ǐ�Ԃ��̂Ƃ�
            if (color == (int) WALL_COLOR.BLUE)
            {
                color = (int) WALL_COLOR.PINK; //�s���N�ɕύX
                isStop = true; //��~�t���O�J�n
                isFlag = (uint) FLAG_KEY.C_CHANGE; //0100
            }
            //�Ǐ�Ԃ��s���N�̂Ƃ�
            else if (color == (int) WALL_COLOR.PINK)
            {
                color = (int) WALL_COLOR.BLUE; //�ɕύX
                isStop = true; //��~�t���O�J�n
                isFlag = (uint) FLAG_KEY.C_CHANGE; //0100
            }
        }

        //���E���]
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.E /*Joystick1Button1*/))
        {
            if (!isInverse)
            {
                //���]���
                transform.position = CB_POS; //���ʂɃZ�b�g
                isChange = true; //�ؑփt���O�J�n
                isStop = true; //��~�t���O�J�n
                isInverse = true; //���]�i�\�ʁ����ʁj
                isFlag = (uint) FLAG_KEY.INVERSE; //0010
            }
            else //�񔽓]���
            {
                transform.position = CF_POS; //�\�ʂɃZ�b�g
                isChange = true; //�ؑփt���O�J�n
                isStop = true; //��~�t���O�J�n
                isInverse = false; //���]�i���ʁ��\�ʁj
                isFlag = (uint) FLAG_KEY.INVERSE; //0010
            }
        }

        //���]
        if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f && Input.GetKey(KeyCode.Q /*Joystick1Button4*/))
        {
            isRotate = (int) DIRECTION.LEFT; //����]
            isFlag = (uint) FLAG_KEY.ROTATE; //0001
        }
        //�t�]
        else if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f && Input.GetKey(KeyCode.W /*Joystick1Button5*/))
        {
            isRotate = (int) DIRECTION.RIGHT; //�E��]
            isFlag = (uint) FLAG_KEY.ROTATE; //0001
        }

        if (Math.Abs(angle) == ONE_CIRCLE) angle = 0.0f; //���������A��]�p�x�����Z�b�g

        LeftRotate(); //����]
        RightRotate(); //�E��]
    }

    private void LeftRotate()
    {
        //����]�̂Ƃ�
        if (isRotate == (int) DIRECTION.LEFT)
        {
            //�p�x�ɂ���ĕ���
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, -ROTATE_QUANTITY); //��]����
                rotate += ROTATE_QUANTITY; //��]�ʂ�^����
            }
            else if (rotate >= ONE_QUARTER)
            {
                isRotate = (int) DIRECTION.NONE; //����]��Ԃɖ߂�
                angle += ONE_QUARTER; //�p�x��90�x
                rotate = 0.0f; //��]�I��
                isFlag = (uint) FLAG_KEY.NONE; //�t���O�֘A�����Z�b�g
            }
        }
    }

    private void RightRotate()
    {
        //�E��]�̂Ƃ�
        if (isRotate == (int) DIRECTION.RIGHT)
        {
            //�p�x�ɂ���ĕ���
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, ROTATE_QUANTITY); //��]����
                rotate += ROTATE_QUANTITY; //��]�ʂ�^����
            }
            else if (rotate >= ONE_QUARTER)
            {
                isRotate = (int) DIRECTION.NONE; //����]��Ԃɖ߂�
                angle -= ONE_QUARTER; //�p�x��-90�x
                rotate = 0.0f; //��]�I��
                isFlag = (uint) FLAG_KEY.NONE; //�t���O�֘A�����Z�b�g
            }
        }
    }
}
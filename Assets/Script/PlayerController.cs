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
    public static bool isReset;
    public static bool isMenu;
    private AudioSource audioSource;
    private int isRotate; //��]�t���O
    private float menuCount;

    // ���ʉ��p�ϐ�
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize(); //������
        audioSource = GetComponent<AudioSource>(); //Component���擾
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("�S�t���O" + isFlag);
        //Debug.Log("�F" + color);
        Debug.Log("�J�E���g" + menuCount);

        AdditionCount(); //���j���[�J�E���g���Z

        if (!isMenu && Input.GetKey(KeyCode.Return)) isFlag = (uint) FLAG_KEY.NONE; //���j���[�t���O�I��

        if (isFlag == (uint) FLAG_KEY.MENU) return; //���j���[�t���O�̂Ƃ��A������̓X�L�b�v

        SubtractCount(); //���j���[�J�E���g���Z

        //���j���[
        if (menuCount == 0.0f && isFlag == (uint) FLAG_KEY.NONE && Input.GetKey(KeyCode.Return))
            isFlag = (uint) FLAG_KEY.MENU; //���j���[�t���O�J�n

        //���Z�b�g
        if (isFlag == (uint) FLAG_KEY.NONE && Input.GetKey(KeyCode.Space))
        {
            isReset = true;
            isFlag = (uint) FLAG_KEY.RESET;
            transform.Rotate(0.0f, 0.0f, angle);
            color = (int) WALL_COLOR.PINK;
            angle = 0.0f; //�J������]�p�x0
            rotate = 0.0f; //��]�p0
            isRotate = (int) DIRECTION.NONE; //����]���
            isInverse = false; //�񔽓]���
            color = (int) WALL_COLOR.PINK;
            menuCount = 0.0f;
        }

        //�F���]
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.W) && !cubeRotate.isWall)
        {
            audioSource.PlayOneShot(sound1); // ���ʉ���炷
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
        if (isFlag == (uint) FLAG_KEY.NONE && !isStop && Input.GetKeyDown(KeyCode.Q /*Joystick1Button1*/))
        {
            audioSource.PlayOneShot(sound2); // ���ʉ���炷
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
        if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f && Input.GetKey(KeyCode.LeftArrow /*Joystick1Button4*/))
        {
            audioSource.PlayOneShot(sound3); // ���ʉ���炷
            isRotate = (int) DIRECTION.LEFT; //����]
            isFlag = (uint) FLAG_KEY.ROTATE; //0001
        }
        //�t�]
        else if (isFlag == (uint) FLAG_KEY.NONE && rotate == 0.0f &&
                 Input.GetKey(KeyCode.RightArrow /*Joystick1Button5*/))
        {
            audioSource.PlayOneShot(sound3); // ���ʉ���炷
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

    //������
    private void Initialize()
    {
        angle = 0.0f; //�J������]�p�x0
        rotate = 0.0f; //��]�p0
        isRotate = (int) DIRECTION.NONE; //����]���
        isInverse = false; //�񔽓]���
        isStop = false; //��~
        isChange = false; //�ؑւȂ�
        color = (int) WALL_COLOR.PINK;
        isFlag = 0b0000; //0000�ɃZ�b�g
        isReset = false;
        isMenu = false;
        menuCount = 0.0f;
    }
}
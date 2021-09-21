using System;
using System.Collections;
using UnityEngine;
using static Call.ConstantValue;

namespace PLAYER
{
    public class cubeRotate : MonoBehaviour
    {
        public static bool isWall; //�ǃt���O
        public static bool isGoal; //�S�[���t���O
        public static uint isResult; //���ʃt���O
        private float angleNum; //�i�[�p�̊p�x

        private Vector3 contactPoints; //�Փˈʒu

        private float cubeAngle; //�p�x
        private float cubeSizeHalf; //�L���[�u�̑傫���̔���
        private float currentAngel; //���݂̊p�x

        private int currentWall; //���݂̕Ǐ��
        private float fallCount; //��������
        private bool isCollision; //�Փ˃t���O
        private bool isFloor; //���t���O
        private bool isRotate; //��]�t���O

        private Vector3 rotateAxis = Vector3.zero; //��
        private Vector3 rotatePoint = Vector3.zero; //���S
        private float stopCount; //��~����
        private Vector3 stopPos; //��~�ʒu
        private float widthB; //���ʂ̉���
        private float widthF; //�O�ʂ̉���

        //�����ʒu�Z�b�g
        private void InitSet(Vector3 pos, Vector3 rot, Vector3 scl)
        {
            transform.position = pos; //�ʒu
            transform.rotation = Quaternion.Euler(rot); //��]�p
            transform.localScale = scl; //�K��
        }

        // Start is called before the first frame update
        private void Start()
        {
            //������
            InitSet(P_POS, P_ROT, P_SCL); //�����ʒu�Z�b�g
            cubeSizeHalf = transform.localScale.x / 2.0f; //���L���[�u
            cubeAngle = 0.0f; //�L���[�u�̊p�x
            currentAngel = 0.0f; //���݁i�J�����́j�̉�]�p�x
            stopCount = 0.0f; //��~����
            isRotate = false; //��]�t���O
            isCollision = false; //�Փ˃t���O
            currentWall = (int) WALL_COLOR.PINK; //���݂̕Ǐ�Ԃ��s���N��
            isWall = false; //�ǃt���O
            isFloor = true; //���t���O
            fallCount = 0.0f; //��������
            isGoal = false; //�S�[���t���O
            isResult = 0b0000; //000
        }

        // Update is called once per frame
        private void Update()
        {
            Debug.Log("������:" + isFloor);

            currentWall = PlayerController.color; //���݂̐F���擾

            if (!isFloor) transform.position -= FALL_SPEED; //���t���O���Ȃ��Ƃ��A����

            //��]�p�x�ƌ��݂̉�]�p�x������ or �ؑփt���O��true�̂Ƃ�
            if (angleNum != currentAngel || PlayerController.isChange)
                isCollision = false;

            //�ؑփt���O��true and ���]�t���O��true�̂Ƃ�
            if (PlayerController.isChange && PlayerController.isInverse)
            {
                SetInversePosition(); //���݈ʒu�𔽓]�ʒu�ɐݒu
                PlayerController.isChange = false; //�ؑփt���O�I��
            }
            else if (PlayerController.isChange && !PlayerController.isInverse)
            {
                SetForwardRotatePosition(); //���݈ʒu�𐳓]�ʒu�ɐݒu
                PlayerController.isChange = false; //�ؑփt���O�I��
            }

            GetResult(); //���ʂ��擾

            InverseStop(); //���]�ɂ���~

            CalcAngel(); //�p�x���v�Z

            if (isCollision) FixFloorPosition(); //�v���C���[���̈ʒu���C��

            if (PlayerController.isStop || !isFloor) return; //��~�t���O��true or ���t���O��false�̂Ƃ��X�L�b�v

            //�Փ˂��Ă��Ȃ��Ƃ�
            if (!PlayerController.isStop && !isCollision && PlayerController.rotate == 0.0f)
            {
                if (isRotate) return; //��]�t���O��true�̂Ƃ��X�L�b�v

                if (PlayerController.angle == 0.0f) LeftMove(); //���Ɉړ�
                else if (PlayerController.angle == 90.0f || PlayerController.angle == -270.0f) UpMove(); //��
                else if (PlayerController.angle == 180.0f || PlayerController.angle == -180.0f) RightMove(); //�E
                else if (PlayerController.angle == 270.0f || PlayerController.angle == -90.0f) DownMove(); //��
                StartCoroutine(MoveCube()); //��]����
                currentAngel = angleNum; //���݂̉�]�p�x��ۑ�
            }
        }

        //�Փ˂����Ƃ�
        private void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //�Փˈʒu���擾

            if (collision.gameObject.tag != "wall") return; //�^�O��wall�̂Ƃ�

            //�ǂ����݂̐F�ȊO�̂Ƃ�
            if (collision.gameObject.layer != currentWall)
            {
                isCollision = true; //�Փ˃t���O�J�n
                transform.position +=
                    new Vector3(
                        transform.position.x - contactPoints.x,
                        0.0f,
                        transform.position.z - contactPoints.z); //�ǂ߂荞�ݖh�~����
                GetStopPosition(); //��~�ʒu�擾
            }
        }

        //�Փ˂��Ă���Ƃ�
        private void OnCollisionStay(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //�Փˈʒu���擾

            //if (collision.gameObject.tag == "floor")
            //{
            //    isFloor = true;
            //}
            //else
            if (collision.gameObject.tag == "fall")
            {
                fallCount += LATE_SPEED; //�ҋ@���x�����Z
                if (fallCount >= LATENCY) isFloor = false; //�������Ԃ��ҋ@���Ԃ��o�߂����Ƃ��A���t���O��false
            }

            if (collision.gameObject.tag == "goal")
            {
                isResult = 0b0001;
                isGoal = true;
            }

            if (collision.gameObject.tag != "wall") return; //�ǈȊO�̂Ƃ��X�L�b�v

            //�ǂ����݂̐F�ȊO�̂Ƃ�
            if (collision.gameObject.layer != currentWall)
            {
                isCollision = true; //�Փ˃t���O�J�n
                transform.position +=
                    new Vector3(
                        transform.position.x - contactPoints.x,
                        0.0f,
                        transform.position.z - contactPoints.z); //�ǂ߂荞�ݖh�~����
                GetStopPosition(); //��~�ʒu�擾
            }

            isWall = true; //�ǃt���O�J�n
        }

        //�ʉ߂��I�����Ƃ�
        private void OnCollisionExit(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //�Փˈʒu���擾

            if (collision.gameObject.tag == "fall") isFloor = false;

            if (collision.gameObject.tag != "wall") return; //�ǈȊO�̂Ƃ��X�L�b�v

            isWall = false; //�ǃt���O�I��
        }

        //���̈ʒu���C��
        private void FixFloorPosition()
        {
            SetStopPosition(); //��~�ʒu�ɐݒu
            Debug.Log("�ՓˁF" + isCollision); //�Փ�
        }

        //���ړ�
        private void LeftMove()
        {
            rotatePoint = transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0.0f);
            rotateAxis = new Vector3(0.0f, 0.0f, 1.0f);
        }

        //�E�ړ�
        private void RightMove()
        {
            rotatePoint = transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0.0f);
            rotateAxis = new Vector3(0.0f, 0.0f, -1.0f);
        }

        //��ړ�
        private void UpMove()
        {
            rotatePoint = transform.position + new Vector3(0.0f, -cubeSizeHalf, cubeSizeHalf);
            rotateAxis = new Vector3(1.0f, 0.0f, 0.0f);
        }

        //���ړ�
        private void DownMove()
        {
            rotatePoint = transform.position + new Vector3(0.0f, -cubeSizeHalf, -cubeSizeHalf);
            rotateAxis = new Vector3(-1.0f, 0.0f, 0.0f);
        }

        //�p�x���v�Z
        private void CalcAngel()
        {
            angleNum = PlayerController.angle; //�J�����̊p�x���i�[
        }

        private IEnumerator MoveCube()
        {
            isRotate = true; //��]�J�n

            //��]����
            var sumAngle = 0.0f;
            while (sumAngle < ONE_QUARTER)
            {
                cubeAngle = ROTATE_SPEED; //��]����p�x����
                sumAngle += cubeAngle; //��]���x�����Z

                if (sumAngle > ONE_QUARTER) cubeAngle -= sumAngle - ONE_QUARTER; //�ߏ�ɉ�]���Ȃ��悤����
                transform.RotateAround(rotatePoint, rotateAxis, cubeAngle); //axis�𒆐S�Ƃ��ĉ�]

                yield return null; //�ꎞ�I�ɒ�~
            }

            isRotate = false; //��]�I��
        }

        //���݈ʒu�𔽓]�ʒu�ɐݒu
        private void SetInversePosition()
        {
            widthF = 2.0f * Math.Abs(transform.position.x - 240.0f) + SPACE; //���݈ʒu���甽�]�ʒu�܂ł̉������擾
            transform.position = new Vector3(
                transform.position.x + widthF,
                10.0f,
                transform.position.z); //�擾�����ʒu�ɐݒu
            GetStopPosition(); //��~�ʒu�擾
        }

        //���݈ʒu�𐳓]�ʒu�ɐݒu
        private void SetForwardRotatePosition()
        {
            widthB = 2.0f * Math.Abs(transform.position.x - 720.0f) + SPACE; //���݈ʒu���甽�]�ʒu�܂ł̉������擾
            transform.position = new Vector3(
                transform.position.x - widthB,
                10.0f,
                transform.position.z); //�擾�����ʒu�ɐݒu
            GetStopPosition(); //��~�ʒu�擾
        }

        //���]�ɂ���~
        private void InverseStop()
        {
            //��~�t���O��true�̂Ƃ�
            if (PlayerController.isStop)
            {
                stopCount += 0.1f; //��~���ԋN��
                if (PlayerController.isFlag == (uint) FLAG_KEY.INVERSE) SetStopPosition(); //��~�ʒu�ɐݒu
            }

            //�J�E���g��STOP_TIME�ȏ�̂Ƃ�
            if (stopCount >= STOP_TIME)
            {
                stopCount = 0.0f; //��~���Ԓ�~
                PlayerController.isStop = false; //��~�t���O�I��
                PlayerController.isFlag = (uint) FLAG_KEY.NONE; //�t���O�֘A�����Z�b�g
            }
        }

        //��~�ʒu�擾
        private void GetStopPosition()
        {
            stopPos = new Vector3(transform.position.x, INITIAL_Y, transform.position.z); //�ʒu�擾
        }

        //��~�ʒu�ɐݒu
        private void SetStopPosition()
        {
            transform.position = new Vector3(stopPos.x, INITIAL_Y, stopPos.z); //���݈ʒu�ɒ�~�ʒu��
            transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); //��]�p�����Z�b�g
        }

        private void GetResult()
        {
            if (transform.position.y <= -300.0f)
            {
                isResult = 0b0010;
                isGoal = true;
            }
        }

        //��O����
        private void ExceptionHandling()
        {
            if (transform.position.y <= 0.0f && isFloor) SetStopPosition();
        }
    }
}
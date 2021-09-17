using System;
using System.Collections;
using UnityEngine;
using static Call.ConstantValue;

namespace PLAYER
{
    public class cubeRotate : MonoBehaviour
    {
        private float angleNum;

        private Vector3 contactPoints;

        private float cubeAngle; //�p�x
        private float cubeSizeHalf; //�L���[�u�̑傫���̔���

        private float currentAngel;

        private float intervalCount;

        private bool isCollision;

        private bool isRotate; //��]�t���O


        private Vector3 rotateAxis = Vector3.zero; //��
        private Vector3 rotatePoint = Vector3.zero; //���S
        private float stopCount;

        private Vector3 stopPos;
        public float widthB;
        public float widthF;

        private void InitSet(Vector3 pos, Vector3 rot, Vector3 scl)
        {
            transform.position = pos;
            transform.rotation = Quaternion.Euler(rot);
            transform.localScale = scl;
        }

        // Start is called before the first frame update
        private void Start()
        {
            InitSet(P_POS, P_ROT, P_SCL); //�����ʒu�Z�b�g
            cubeSizeHalf = transform.localScale.x / 2.0f;
            cubeAngle = 0.0f;
            currentAngel = 0.0f;
            intervalCount = 0.0f;
            isRotate = false;
            isCollision = false;
            stopCount = 0.0f;
        }

        // Update is called once per frame
        private void Update()
        {
            //��]�p�x�ƌ��݂̉�]�p�x������ or �ؑփt���O��true�̂Ƃ�
            if (angleNum != currentAngel || PlayerController.change)
                isCollision = false;

            //�ؑփt���O��true and ���]�t���O��true�̂Ƃ�
            if (PlayerController.change && PlayerController.inverse)
            {
                SetInversePosition(); //���݈ʒu�𔽓]�ʒu�ɐݒu
                PlayerController.change = false; //�ؑփt���O�I��
            }
            else if (PlayerController.change && !PlayerController.inverse)
            {
                SetForwardRotatePosition(); //���݈ʒu�𐳓]�ʒu�ɐݒu
                PlayerController.change = false; //�ؑփt���O�I��
            }

            InverseStop(); //���]�ɂ���~

            CalcAngel(); //�p�x���v�Z

            if (isCollision) FixFloorPosition(); //�v���C���[���̈ʒu���C��

            if (PlayerController.isStop) return; //��~�t���O��true�̂Ƃ��X�L�b�v

            Debug.Log("�쓮��");

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

        //�Փ˔���
        private void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts) contactPoints = contact.point; //�Փˈʒu���擾

            //�^�O��wall�̂Ƃ�
            if (collision.gameObject.tag == "wall")
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

        private void FixFloorPosition()
        {
            SetStopPosition(); //��~�ʒu�ɐݒu
            Debug.Log("�ՓˁF" + isCollision); //�Փ�
        }

        private void Counting()
        {
            //�ǂɂԂ�������J�E���g
            if (intervalCount <= INTERVAL)
                intervalCount += 0.1f; //�J�E���g�J�n
            else
                //isCollision = false; //�Փ˂��Ă��Ȃ�
                intervalCount = 0.0f; //�J�E���g���Z�b�g
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
                cubeAngle = SPEED; //��]����p�x����
                sumAngle += cubeAngle; //��]���x�����Z

                if (sumAngle > ONE_QUARTER) cubeAngle -= sumAngle - ONE_QUARTER; //�ߏ�ɉ�]���Ȃ��悤����
                transform.RotateAround(rotatePoint, rotateAxis, cubeAngle); //axis�𒆐S�Ƃ��ĉ�]

                yield return null; //�ꎞ�I�ɒ�~
            }

            isRotate = false; //��]�I��
        }

        //���݈ʒu�𔽓]�ʒu�ɐݒu
        public void SetInversePosition()
        {
            widthF = 2.0f * Math.Abs(transform.position.x - 240.0f) + SPACE; //���݈ʒu���甽�]�ʒu�܂ł̉������擾
            transform.position = new Vector3(
                transform.position.x + widthF,
                10.0f,
                transform.position.z); //�擾�����ʒu�ɐݒu
            GetStopPosition(); //��~�ʒu�擾
        }

        //���݈ʒu�𐳓]�ʒu�ɐݒu
        public void SetForwardRotatePosition()
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
                SetStopPosition(); //��~�ʒu�ɐݒu
            }

            //�J�E���g��STOP_TIME�ȏ�̂Ƃ�
            if (stopCount >= STOP_TIME)
            {
                stopCount = 0.0f; //��~���Ԓ�~
                PlayerController.isStop = false; //��~�t���O�I��
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
    }
}
using UnityEngine;
using static Call.ConstantValue;

public class ChangeColor : MonoBehaviour
{
    private Material mat;

    // Start is called before the first frame update
    private void Start()
    {
        //�����ݒ�
        mat = GetComponent<Renderer>().material; //�}�e���A�����擾
        mat.color = PINK_COLOR; //�s���N�ɐݒ�
    }

    // Update is called once per frame
    private void Update()
    {
        //�F���]�̏�Ԃɂ���āA�}�e���A����ύX
        if (PlayerController.color == (int) WALL_COLOR.PINK) mat.color = PINK_COLOR; //�s���N
        else if (PlayerController.color == (int) WALL_COLOR.BLUE) mat.color = BLUE_COLOR; //��
    }
}
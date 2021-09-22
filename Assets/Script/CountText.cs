using UnityEngine;
using UnityEngine.UI;
using static Call.ConstantValue;

public class CountText : MonoBehaviour
{
    // Start is called before the first frame update
    public static int maxCount;
    private bool curStop; //���݂̃J�E���g

    //public bool KeyFlag;
    public Text textComponent;

    // Start is called before the first frame update
    private void Start()
    {
        curStop = false;
        maxCount = LEFTOVER;
    }

    // Update is called once per frame
    private void Update()
    {
        CountFlag();
    }

    private void CountFlag()
    {
        textComponent.text = "" + maxCount; //�����J�E���g

        if (!curStop
            && PlayerController.isFlag != (uint) FLAG_KEY.NONE
            && PlayerController.isFlag != (uint) FLAG_KEY.ROTATE2) CulcCount();

        //��~�t���O��false�ɐ؂�ւ�����Ƃ�
        if (PlayerController.isStop != curStop)
            curStop = false; //���݂̒�~��Ԃ�false
    }

    private void CulcCount()
    {
        if (maxCount <= 0) return;

        curStop = true; //���݂̒�~���true
        maxCount--; //�J�E���g�����Z

        if (PlayerController.isFlag != (uint) FLAG_KEY.ROTATE) return;
        PlayerController.isFlag = (uint) FLAG_KEY.ROTATE2; //��]�t���O��2�i�K�ڂɈڍs
    }
}
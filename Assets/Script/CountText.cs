using UnityEngine;
using UnityEngine.UI;
using static Call.ConstantValue;

public class CountText : MonoBehaviour
{
    // Start is called before the first frame update
    public static int maxCount;
    private bool curStop; //現在のカウント

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
        textComponent.text = "" + maxCount; //文字カウント

        if (!curStop
            && PlayerController.isFlag != (uint) FLAG_KEY.NONE
            && PlayerController.isFlag != (uint) FLAG_KEY.ROTATE2) CulcCount();

        //停止フラグがfalseに切り替わったとき
        if (PlayerController.isStop != curStop)
            curStop = false; //現在の停止状態をfalse
    }

    private void CulcCount()
    {
        if (maxCount <= 0) return;

        curStop = true; //現在の停止状態true
        maxCount--; //カウントを減算

        if (PlayerController.isFlag != (uint) FLAG_KEY.ROTATE) return;
        PlayerController.isFlag = (uint) FLAG_KEY.ROTATE2; //回転フラグを2段階目に移行
    }
}
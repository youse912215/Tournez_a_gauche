using UnityEngine;
using static Call.ConstantValue;

public class ChangeColor : MonoBehaviour
{
    private Material mat;

    // Start is called before the first frame update
    private void Start()
    {
        //初期設定
        mat = GetComponent<Renderer>().material; //マテリアルを取得
        mat.color = PINK_COLOR; //ピンクに設定
    }

    // Update is called once per frame
    private void Update()
    {
        //色反転の状態によって、マテリアルを変更
        if (PlayerController.color == (int) WALL_COLOR.PINK) mat.color = PINK_COLOR; //ピンク
        else if (PlayerController.color == (int) WALL_COLOR.BLUE) mat.color = BLUE_COLOR; //青
    }
}
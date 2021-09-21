using PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Call.ConstantValue;

public class GameManager : MonoBehaviour
{
    private int sceneName; //シーン番号

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1920, 1080, false); //画面サイズ
        Application.targetFrameRate = 60; //FPS固定
        sceneName = (int) SCENE_NAME.TITLE;
    }

    // Update is called once per frame
    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().buildIndex; //現在のシーン番号を保存

        //タイトルシーンのとき
        if (sceneName == (int) SCENE_NAME.TITLE)
            //if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene("sunny"); //難易度SUNNYへ
            //else
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SceneManager.LoadScene("cloudy"); //難易度CLOUDYへ
        //else if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene("rainy"); //難易度RAINYへ

        if (cubeRotate.isGoal)
        {
            SceneManager.LoadScene("result");
            cubeRotate.isGoal = false;
        }

        if (sceneName == (int) SCENE_NAME.RESULT)
            if (Input.anyKey)
                SceneManager.LoadScene("title");
    }
}
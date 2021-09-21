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
        Debug.Log(sceneName);

        sceneName = SceneManager.GetActiveScene().buildIndex; //現在のシーン番号を保存


        //タイトルシーンのとき
        if (sceneName == (int) SCENE_NAME.TITLE)
        {
            if (Input.GetKeyDown(KeyCode.A)) SceneManager.LoadScene("sunny"); //難易度SUNNYへ
            else if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("cloudy"); //難易度CLOUDYへ
            else if (Input.GetKeyDown(KeyCode.D)) SceneManager.LoadScene("rainy"); //難易度RAINYへ
        }
    }
}
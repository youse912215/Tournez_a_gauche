using PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Call.ConstantValue;
using static Call.CommonFunction;

public class GameManager : MonoBehaviour
{
    public static int sceneName; //シーン番号
    private AudioSource audioSource;
    private GameObject menu;
    private GameObject operation;

    public AudioClip select;

    private GameObject volume;

    // Start is called before the first frame update
    private void Start()
    {
        operation = GameObject.Find("Operation");
        operation.SetActive(false);
        volume = GameObject.Find("Slider");
        volume.SetActive(false);
        menu = GameObject.Find("Menu");
        menu.SetActive(false);
        Screen.SetResolution(1920, 1080, false); //画面サイズ
        Application.targetFrameRate = 60; //FPS固定
        sceneName = (int) SCENE_NAME.TITLE;
        audioSource = GetComponent<AudioSource>(); //Componentを取得
    }

    // Update is called once per frame
    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().buildIndex; //現在のシーン番号を保存

        //タイトルシーンのとき
        if (sceneName == (int) SCENE_NAME.TITLE)
        {
            Debug.Log("タイトル");

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("sunny"); //難易度SUNNYへ
                CountText.maxCount = SetCountValue((int) SCENE_NAME.SUNNY);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("cloudy"); //難易度CLOUDYへ
                CountText.maxCount = SetCountValue((int) SCENE_NAME.CLOUDY);
                Debug.Log("雲：" + CountText.maxCount);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("rainy"); //難易度RAINYへ
                CountText.maxCount = SetCountValue((int) SCENE_NAME.RAINY);
            }
        }

        if (sceneName == (int) SCENE_NAME.RESULT) menu.SetActive(false);


        if (sceneName == (int) SCENE_NAME.RESULT)
            if (Input.anyKey)
                SceneManager.LoadScene("title");

        if (sceneName == (int) SCENE_NAME.TITLE || sceneName == (int) SCENE_NAME.RESULT) return;

        menu.SetActive(true);

        if (cubeRotate.isGoal)
        {
            SceneManager.LoadScene("result");
            cubeRotate.isGoal = false;
        }

        if (PlayerController.isFlag == (uint) FLAG_KEY.MENU)
        {
            PlayerController.isMenu = true;
            operation.SetActive(true);
            volume.SetActive(true);
        }
        else
        {
            operation.SetActive(false);
            volume.SetActive(false);
        }
    }
}
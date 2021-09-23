using PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Call.ConstantValue;
using static Call.CommonFunction;

public class GameManager : MonoBehaviour
{
    public static int sceneName; //�V�[���ԍ�
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
        Screen.SetResolution(1920, 1080, false); //��ʃT�C�Y
        Application.targetFrameRate = 60; //FPS�Œ�
        sceneName = (int) SCENE_NAME.TITLE;
        audioSource = GetComponent<AudioSource>(); //Component���擾
    }

    // Update is called once per frame
    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().buildIndex; //���݂̃V�[���ԍ���ۑ�

        //�^�C�g���V�[���̂Ƃ�
        if (sceneName == (int) SCENE_NAME.TITLE)
        {
            Debug.Log("�^�C�g��");

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("sunny"); //��ՓxSUNNY��
                CountText.maxCount = SetCountValue((int) SCENE_NAME.SUNNY);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("cloudy"); //��ՓxCLOUDY��
                CountText.maxCount = SetCountValue((int) SCENE_NAME.CLOUDY);
                Debug.Log("�_�F" + CountText.maxCount);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                audioSource.PlayOneShot(select);
                SceneManager.LoadScene("rainy"); //��ՓxRAINY��
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
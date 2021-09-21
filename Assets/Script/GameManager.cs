using UnityEngine;
using UnityEngine.SceneManagement;
using static Call.ConstantValue;

public class GameManager : MonoBehaviour
{
    private int sceneName; //�V�[���ԍ�

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1920, 1080, false); //��ʃT�C�Y
        Application.targetFrameRate = 60; //FPS�Œ�
        sceneName = (int) SCENE_NAME.TITLE;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(sceneName);

        sceneName = SceneManager.GetActiveScene().buildIndex; //���݂̃V�[���ԍ���ۑ�


        //�^�C�g���V�[���̂Ƃ�
        if (sceneName == (int) SCENE_NAME.TITLE)
        {
            if (Input.GetKeyDown(KeyCode.A)) SceneManager.LoadScene("sunny"); //��ՓxSUNNY��
            else if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("cloudy"); //��ՓxCLOUDY��
            else if (Input.GetKeyDown(KeyCode.D)) SceneManager.LoadScene("rainy"); //��ՓxRAINY��
        }
    }
}
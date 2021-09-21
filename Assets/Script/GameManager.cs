using PLAYER;
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
        sceneName = SceneManager.GetActiveScene().buildIndex; //���݂̃V�[���ԍ���ۑ�

        //�^�C�g���V�[���̂Ƃ�
        if (sceneName == (int) SCENE_NAME.TITLE)
            //if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene("sunny"); //��ՓxSUNNY��
            //else
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SceneManager.LoadScene("cloudy"); //��ՓxCLOUDY��
        //else if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene("rainy"); //��ՓxRAINY��

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
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 scale;

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1920, 1080, false); //画面サイズ
        Application.targetFrameRate = 60; //FPS固定
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
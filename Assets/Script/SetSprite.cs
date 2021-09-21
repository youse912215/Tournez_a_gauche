using PLAYER;
using UnityEngine;

public class SetSprite : MonoBehaviour
{
    private GameObject clear;
    private GameObject over;

    // Start is called before the first frame update
    private void Start()
    {
        clear = GameObject.Find("GameClear");
        over = GameObject.Find("GameOver");
    }

    // Update is called once per frame
    private void Update()
    {
        if (cubeRotate.isResult == 0b0001)
        {
            Debug.Log("CLEAR");
            clear.SetActive(true);
            over.SetActive(false);
        }
        else if (cubeRotate.isResult == 0b0010)
        {
            Debug.Log("OVER");
            clear.SetActive(false);
            over.SetActive(true);
        }
    }
}
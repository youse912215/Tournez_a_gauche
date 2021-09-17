using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 scale;

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1920, 1080, false); //âÊñ ÉTÉCÉY
        Application.targetFrameRate = 60; //FPSå≈íË
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
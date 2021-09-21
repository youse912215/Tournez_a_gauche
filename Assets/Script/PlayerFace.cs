using PLAYER;
using UnityEngine;
using static Call.ConstantValue;

public class PlayerFace : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (cubeRotate.isResult == 0b0001) transform.rotation = P_CLEAR;
        else if (cubeRotate.isResult == 0b0010) transform.rotation = P_OVER;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
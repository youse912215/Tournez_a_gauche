using UnityEngine;
using static Call.ConstantValue;

public class MoveCamera : MonoBehaviour
{
    private GameObject back;
    private Camera cameraBack;
    private Camera cameraFront;
    private GameObject front;
    private Vector3 offset;
    private GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("player");
        front = GameObject.Find("Sub Camera Front");
        cameraFront = front.GetComponent<Camera>();
        back = GameObject.Find("Sub Camera Back");
        cameraBack = back.GetComponent<Camera>();
        transform.position = CF_POS; //カメラを初期位置にセット
        offset = transform.position - player.transform.position;
        cameraFront.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(
            player.transform.position.x + offset.x,
            transform.position.y,
            player.transform.position.z + offset.z
        );

        if (PlayerController.isInverse)
        {
            cameraFront.enabled = false;
            cameraBack.enabled = true;
        }
        else
        {
            cameraBack.enabled = false;
            cameraFront.enabled = true;
        }
    }
}
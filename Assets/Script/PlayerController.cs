using System;
using UnityEngine;
using static Call.ConstantValue;

public class PlayerController : MonoBehaviour
{
    //âÒì]å¸Ç´
    public enum DIRECTION
    {
        NONE, //ñ≥âÒì] 
        LEFT, //ç∂âÒì]
        RIGHT //âEâÒì]
    }

    public static float angle;
    public static float rotate;
    public static bool inverse;
    public static bool change;
    public static bool isStop;

    private int rotateFlag;

    // Start is called before the first frame update
    private void Start()
    {
        rotateFlag = 0;
        angle = 0.0f;
        inverse = false;

        transform.position = CF_POS;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isStop && Input.GetKeyDown(KeyCode.E /*Joystick1Button1*/))
        {
            if (!inverse)
            {
                //îΩì]èÛë‘
                transform.position = CB_POS;
                change = true;
                isStop = true;
                inverse = true;
            }
            else
            {
                //îÒîΩì]èÛë‘
                transform.position = CF_POS;
                change = true;
                isStop = true;
                inverse = false;
            }
        }

        if (rotate == 0.0f && Input.GetKey(KeyCode.Q /*Joystick1Button4*/))
            rotateFlag = (int) DIRECTION.LEFT;
        else if (rotate == 0.0f && Input.GetKey(KeyCode.W /*Joystick1Button5*/))
            rotateFlag = (int) DIRECTION.RIGHT;

        if (Math.Abs(angle) == ONE_CIRCLE) angle = 0.0f;

        if (rotateFlag == (int) DIRECTION.LEFT)
        {
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, -ROTATE_QUANTITY);
                rotate += ROTATE_QUANTITY;
            }
            else if (rotate >= ONE_QUARTER)
            {
                rotateFlag = (int) DIRECTION.NONE;
                angle += ONE_QUARTER;
                rotate = 0.0f;
            }
        }
        else if (rotateFlag == (int) DIRECTION.RIGHT)
        {
            if (rotate < ONE_QUARTER)
            {
                transform.Rotate(0.0f, 0.0f, ROTATE_QUANTITY);
                rotate += ROTATE_QUANTITY;
            }
            else if (rotate >= ONE_QUARTER)
            {
                rotateFlag = (int) DIRECTION.NONE;
                angle -= ONE_QUARTER;
                rotate = 0.0f;
            }
        }
    }
}
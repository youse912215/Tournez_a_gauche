using UnityEngine;
using static Call.ConstantValue;

/*呼び出し用スクリプト*/
namespace Call
{
    public class CommonFunction : MonoBehaviour
    {
        public static int SetCountValue(int scene)
        {
            return HANDLING[scene];
        }
    }
}
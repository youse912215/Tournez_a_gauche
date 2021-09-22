using UnityEngine;
using UnityEngine.UI;

//Sliderを使用するために必要

[RequireComponent(typeof(Slider))]
public class volume : MonoBehaviour
{
    [SerializeField] private readonly float m_ScroolSpeed = 0.5f; //キー入力で調整バーを動かすスピード
    [SerializeField] private bool m_isInput; //キー入力で調整バーを動かせるようにするか

    private Slider m_Slider; //音量調整用スライダー

    private float v;

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        m_Slider.value = AudioListener.volume;
        //スライダーの値が変更されたら音量も変更する
        m_Slider.onValueChanged.AddListener(sliderValue => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        m_Slider.onValueChanged.RemoveAllListeners();
        v = 0.0f;
    }

    //キー入力による操作　いらないなら削除してもOK
    private void Update()
    {
        v = m_Slider.value;
        Debug.Log(v);

        if (m_isInput)
        {
            if (Input.GetKey(KeyCode.DownArrow)) v -= m_ScroolSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.UpArrow)) v += m_ScroolSpeed * Time.deltaTime;
        }

        v = Mathf.Clamp(v, 0, 1);
        m_Slider.value = v;
    }
}
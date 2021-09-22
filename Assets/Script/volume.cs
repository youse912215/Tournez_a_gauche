using UnityEngine;
using UnityEngine.UI;

//Slider���g�p���邽�߂ɕK�v

[RequireComponent(typeof(Slider))]
public class volume : MonoBehaviour
{
    [SerializeField] private readonly float m_ScroolSpeed = 0.5f; //�L�[���͂Œ����o�[�𓮂����X�s�[�h
    [SerializeField] private bool m_isInput; //�L�[���͂Œ����o�[�𓮂�����悤�ɂ��邩

    private Slider m_Slider; //���ʒ����p�X���C�_�[

    private float v;

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        m_Slider.value = AudioListener.volume;
        //�X���C�_�[�̒l���ύX���ꂽ�特�ʂ��ύX����
        m_Slider.onValueChanged.AddListener(sliderValue => AudioListener.volume = sliderValue);
    }

    private void OnDisable()
    {
        m_Slider.onValueChanged.RemoveAllListeners();
        v = 0.0f;
    }

    //�L�[���͂ɂ�鑀��@����Ȃ��Ȃ�폜���Ă�OK
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
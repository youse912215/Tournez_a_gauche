using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class volume : MonoBehaviour
{
    private Slider slider; //���ʃX���C�_�[
    private float vol; //�{�����[��
    private const float min = 0.0f; //�ŏ��l
    private const float max = 1.0f; //�ő�l

    [SerializeField] private readonly float m_ScroolSpeed = 1.5f; //�X�s�[�h
    [SerializeField] private bool m_isInput; //�L�[���̓t���O

    private void Update()
    {
        vol = slider.value;

        if (m_isInput) InputKeys();
        slider.value = Mathf.Clamp(vol, min, max);
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioListener.volume;
    }

    private void OnDisable()
    {
        //������
        slider.onValueChanged.RemoveAllListeners();
        vol = 0.0f;
    }

    private void OnEnable()
    {
        slider.value = AudioListener.volume; //���ʕύX
        slider.onValueChanged.AddListener(sliderValue => AudioListener.volume = sliderValue);
    }

    private void InputKeys()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Joystick1Button4))
                vol -= m_ScroolSpeed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Joystick1Button5))
                vol += m_ScroolSpeed * Time.deltaTime;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // ����ģʽ�����������ű�����
    public AudioSource audioSource; // ��ƵԴ����Ҫ��Inspector��ָ��
    public Button soundButton; // ������ť����Ҫ��Inspector��ָ��
    public Sprite soundOnSprite; // ������ʱ��ͼƬ����Ҫ��Inspector��ָ��
    public Sprite soundOffSprite; // ������ʱ��ͼƬ����Ҫ��Inspector��ָ��

    private void Awake()
    {
        // ����ģʽ��ʼ��
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // ���ð�ť�ĳ�ʼ״̬
        audioSource.mute = false; // ������Ϸ��ʼʱ�����ǿ�����
        soundButton.GetComponent<Image>().sprite = soundOnSprite;
    }

    public void ToggleSound()
    {
        // �л�����״̬
        bool currentMuteState = audioSource.mute;
        audioSource.mute = !currentMuteState;

        // �л���ťͼƬ
        if (audioSource.mute)
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }

        // ����mute״̬���Ż�ֹͣ��Ƶ
        if (audioSource.mute)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}

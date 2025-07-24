using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // 单例模式，方便其他脚本调用
    public AudioSource audioSource; // 音频源，需要在Inspector中指定
    public Button soundButton; // 声音按钮，需要在Inspector中指定
    public Sprite soundOnSprite; // 音量开时的图片，需要在Inspector中指定
    public Sprite soundOffSprite; // 音量关时的图片，需要在Inspector中指定

    private void Awake()
    {
        // 单例模式初始化
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
        // 设置按钮的初始状态
        audioSource.mute = false; // 假设游戏开始时声音是开启的
        soundButton.GetComponent<Image>().sprite = soundOnSprite;
    }

    public void ToggleSound()
    {
        // 切换音量状态
        bool currentMuteState = audioSource.mute;
        audioSource.mute = !currentMuteState;

        // 切换按钮图片
        if (audioSource.mute)
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }

        // 根据mute状态播放或停止音频
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Vedio : MonoBehaviour
{

    private VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.playOnAwake = false;
       // player.audioOutputMode = VideoAudioOutputMode.None; // 关闭音频输出
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // 设置音频输出模式为VideoPlayer
          /* player.audioOutputMode = VideoAudioOutputMode.AudioSource;
            // 确保有一个AudioSource组件附加到同一个游戏对象上
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                player.SetTargetAudioSource(0, audioSource);
            }

            // 开始播放视频*/
           
            player.Play();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            player.Pause();
        }

       
    }
}

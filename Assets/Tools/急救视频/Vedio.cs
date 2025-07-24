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
       // player.audioOutputMode = VideoAudioOutputMode.None; // �ر���Ƶ���
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // ������Ƶ���ģʽΪVideoPlayer
          /* player.audioOutputMode = VideoAudioOutputMode.AudioSource;
            // ȷ����һ��AudioSource������ӵ�ͬһ����Ϸ������
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                player.SetTargetAudioSource(0, audioSource);
            }

            // ��ʼ������Ƶ*/
           
            player.Play();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            player.Pause();
        }

       
    }
}

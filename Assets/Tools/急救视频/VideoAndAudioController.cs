using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoAndAudioController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ��קVideo Player������ֶ�

    private bool isPlaying;
    public GameObject promptUI; // ��ʾ��Ƶ��UI��GameObject

    void Start()
    {
        // ȷ����Ƶ׼����
        videoPlayer.Prepare();
        videoPlayer.errorReceived += HandleVideoError;
    }

    public void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
            isPlaying = true;
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            isPlaying = false;
        }
    }

    public void HideUI()
    {
        promptUI.SetActive(false);
    }



    private void HandleVideoError(VideoPlayer source, string message)
    {
        Debug.LogError("Video Player Error: " + message);
    }
}
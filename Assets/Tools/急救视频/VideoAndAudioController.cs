using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoAndAudioController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 拖拽Video Player到这个字段

    private bool isPlaying;
    public GameObject promptUI; // 显示视频的UI的GameObject

    void Start()
    {
        // 确保视频准备好
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
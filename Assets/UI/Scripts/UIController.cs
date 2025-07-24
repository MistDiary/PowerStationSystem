using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Camera playerCamera; // 玩家的摄像机
    public GameObject promptPanel; // 提示面板的GameObject
    public float distanceFromCamera; // 面板距离摄像机的距离
    public int triggerAreaID; // 触发区域的唯一标识符
    private bool isPanelActive = false; // 面板是否激活的标志
    public AudioSource audioSource;
    public AudioClip audioClip;
    // 当其他碰撞体进入此触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        if (!isPanelActive && other.CompareTag("player")) // 假设玩家有一个"Player"标签
        {
            ShowPromptPanel();
            audioSource.PlayOneShot(audioClip);
        }
    }

    // 当其他碰撞体离开此触发器时调用
    private void OnTriggerExit(Collider other)
    {
        if (isPanelActive && other.CompareTag("player"))
        {
            HidePromptPanel();
        }
    }

    void ShowPromptPanel()
    {
        // 设置面板位置在摄像机前方指定距离
        promptPanel.transform.position = playerCamera.transform.position + playerCamera.transform.forward * distanceFromCamera;
        // 设置面板朝向摄像机
        promptPanel.transform.rotation = playerCamera.transform.rotation;
        // 激活面板
        promptPanel.SetActive(true);
        isPanelActive = true;
    }

    void HidePromptPanel()
    {
        // 隐藏面板
        promptPanel.SetActive(false);
        isPanelActive = false;
    }
    // 用于外部调用以显示面板的方法
    public void DisplayPanelForArea(int areaID)
    {
        if (areaID == triggerAreaID)
        {
            ShowPromptPanel();
        }
    }

    public void HidePanelForArea(int areaID)
    {
        if (areaID == triggerAreaID)
        {
            HidePromptPanel();
        }
    }
}
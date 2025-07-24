using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class triggerPanel : MonoBehaviour
{
    public GameObject panel; // 要显示的面板

    private void OnTriggerEnter(Collider other)
    {
        // 当玩家进入触发区域时显示面板\
        Debug.Log("触碰");
        if (other.CompareTag("player")) // 确保只有带有"Player"标签的对象能触发
        {
            panel.SetActive(true); // 显示面板
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当玩家离开触发区域时隐藏面板
        if (other.CompareTag("player"))
        {
            panel.SetActive(false); // 隐藏面板
        }
    }
}
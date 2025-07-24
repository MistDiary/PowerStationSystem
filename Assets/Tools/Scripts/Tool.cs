using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.UI; // 添加这行代码





public class Tool : MonoBehaviour
{
    public int correctToolIndex = 2; // 正确工具的类型
    public GameObject[] toolModels; // 工具模型数组
    public Button[] toolButtons; // 四个工具按钮
   // public int toolType;
    public ToolSelectionManager toolSelectionManager;

    void Start()
    {
        // 假设ToolSelectionManager是单例模式或者全局访问
        toolSelectionManager = FindObjectOfType<ToolSelectionManager>();
    }

    // 假设这个方法被调用当玩家选择了一个工具
    public void OnToolSelected(int index)
    {
        // 显示对应工具在人物手中
        foreach (GameObject tool in toolModels)
        {
            tool.SetActive(false);
        }
        // 确保索引有效
        if (index >= 0 && index < toolModels.Length)
        {
            toolModels[index].SetActive(true);
        }

        // 检查是否选择了正确的工具
        if (index == correctToolIndex)
        {
            // 正确选择逻辑
            Debug.Log("Correct Tool selected!");
        }
        else
        {
            Debug.Log("Wrong Tool selected!");
            // 报告错误
            toolSelectionManager.ReportError("选择工具错误", 10);

        }


    }
}
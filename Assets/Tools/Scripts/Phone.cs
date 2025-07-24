using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 添加这行代码
public class Phone : MonoBehaviour
{
    public int correctToolIndex = 2; // 正确工具的索引
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
       
        // 检查是否选择了正确的工具
        if (index == correctToolIndex)
        {
            // 正确选择逻辑
            Debug.Log("Correct tool selected!");
        }
        else
        {
            Debug.Log("Wrong tool selected!");
            // 报告错误
            toolSelectionManager.ReportError("Incorrect Number selected", 10);

        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 添加这行代码


public class ToolSelector : MonoBehaviour
{
    // public WorkerController workerController;
    public Canvas canvas; // 引用Canvas
    public float speed = 5f; // 动画速度
    public float maxYPosition = 30f; // 错误提示达到的最大Y坐标
    public GameObject[] toolModels; // 四个工具的模型
    public Button[] toolButtons; // 四个工具按钮
    public Text scoreText; // 显示得分的Text组件
    public Text errorText; // 错误提示的Text组件
    public GameObject errorPanel; // 错误提示的面板
    public GameObject ToolPanel;//工具选择的面板

    // private int score = 100; // 初始分数
    private int correctToolIndex = 2; // 假设第三个工具是正确的
    private bool isMovingErrorPanel = false;



    void Update()
    {
            

            if (isMovingErrorPanel)
            {
                RectTransform errorRectTransform = errorPanel.GetComponent<RectTransform>();
                Vector2 newPosition = errorRectTransform.anchoredPosition;
                newPosition.y += Time.deltaTime * speed;
                errorRectTransform.anchoredPosition = newPosition;

                if (errorRectTransform.anchoredPosition.y > maxYPosition)
                {
                    isMovingErrorPanel = false;
                    errorPanel.SetActive(false);
                }
            }
            
        

    }

    public void SelectTool(int index)
    {
        // 显示对应工具在人物手中
        foreach (GameObject tool in toolModels)
        {
            tool.SetActive(false);
        }
        toolModels[index].SetActive(true);

        // 检查是否选择了正确的工具
        if (index == correctToolIndex)
        {
            // 正确选择逻辑
            Debug.Log("Correct tool selected!");
        }
        else
        {
            Debug.Log("Wrong tool selected!");
            errorPanel.SetActive(true);
            isMovingErrorPanel = true; // 开始移动错误提示面板
                                       // 重置Panel的位置
            RectTransform errorRectTransform = errorPanel.GetComponent<RectTransform>();
            errorRectTransform.anchoredPosition = new Vector2(0.4f, 0.5f); // 根据需要调整初始位置

        }
    }
    public void OnButtonClicked()
    {
        if (ToolPanel != null)
        {
            
            ToolPanel.SetActive(false); // 设置Panel的Active属性为false来隐藏它
           
        }
    }
}
    

  
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class HidePanel : MonoBehaviour
{
    public GameObject player; // 在Inspector中赋值为玩家对象
    public WorkerController workerController;
    public GameObject panelToHide; // 用于在Inspector中拖拽赋值的Panel对象
    public ArrowDong arrowDong; // 引用ArrowDong脚本
    public Button soundButton; // 第一个按钮
    void Start()
    {
        workerController.enabled = false; // 在游戏开始时禁用玩家控制
        // 确保开始时按钮是不可见的
        soundButton.gameObject.SetActive(false);
        if (arrowDong != null)
        {
            arrowDong.shouldDrawButtons = false;
        }
    }
    // 按钮点击时调用此方法
    public void OnButtonClicked()
    {
        if (panelToHide != null)
        {
            Debug.Log("StartGame button clicked"); // 添加调试日志
            panelToHide.SetActive(false); // 设置Panel的Active属性为false来隐藏它
   
            workerController.enabled = true; // 启用玩家控制
            soundButton.gameObject.SetActive(true);
            // 显示路径按钮
            if (arrowDong != null)
            {
                arrowDong.shouldDrawButtons = true;
            }
        }
    }
}

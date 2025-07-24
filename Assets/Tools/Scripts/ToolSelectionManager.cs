using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class ToolSelectionManager : MonoBehaviour
{
    
    public GameObject errorPanel; // 错误提示面板
    public Text errorText; // 错误提示文本
    public Text scoreText; // 得分文本
    public GameObject summaryPanel; // 总结面板
    public Text summaryText; // 总结文本
    public Text CommentText; // 评语文本

    public GameObject ToolPanel1;//工具选择的面板
    public GameObject ToolPanel2;//工具选择的面板
    public GameObject ToolPanel3;//工具选择的面板
    public GameObject LanGan;//布置栏杆

    private bool isMovingErrorPanel = false;
    public Canvas canvas; // 引用Canvas
    public float speed = 5f; // 动画速度
    public float maxYPosition = 30f; // 错误提示达到的最大Y坐标
    public AudioClip soundEffect2; // 音效
   // private AudioSource audioSource1; // 音频源
    public AudioSource audioSource2; // 音频源


    private int score = 100; // 初始分数
    private string summary = "成绩记录:\n"; // 总结信息
    private string Comment ; // 总结信息
    private bool isLanGanActive = false;
    private bool isSoundActive = false;
    private float timer = 0.0f;


    void Start()
    {
        errorPanel.SetActive(false);
        summaryPanel.SetActive(false);
        audioSource2 = GetComponent<AudioSource>();
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }
        audioSource2.clip = soundEffect2;
    }

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
                // 重置Panel的位置
              //  RectTransform errorRectTransform = errorPanel.GetComponent<RectTransform>();
                errorRectTransform.anchoredPosition = new Vector2(0.4f, 0.5f); // 根据需要调整初始位置
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))//检测是否按下Q键布置防护栏
        {
            LanGan.SetActive(true);
            isLanGanActive = true;
            timer = 0.0f; // 重置计时器

            
        }

        if (isLanGanActive)
        {
            timer += Time.deltaTime; // 更新计时器
            if (timer >= 2.0f) // 如果计时器达到2秒
            {

                if (!audioSource2.isPlaying)
                {
                    audioSource2.Play();
                }
                isSoundActive = true;
                isLanGanActive = false; // 重置标志位，防止重复激活
                timer = 0.0f; // 重置计时器
            }
        }

        if (isSoundActive) 
                {
            timer += Time.deltaTime; // 更新计时器
            if (timer >= 2.0f) // 如果计时器达到2秒
            {
                ShowSummary();
                isSoundActive = false;// 重置标志位，防止重复激活
                timer = 0.0f; // 重置计时器
            }
        }



    }

    // 外部调用的方法，用于报告错误
    public void ReportError(string error, int points)
    {
       
        // 更新错误提示文本
        errorText.text = error;
        // 扣分
        DeductPoints(points);
        // 更新总结信息
        summary += error + ": -" + points + " points\n";
        // 显示错误提示面板
        errorPanel.SetActive(true);
        isMovingErrorPanel = true; // 开始移动错误提示面板
                                  
    }

    // 扣分方法
    private void DeductPoints(int points)
    {
        score -= points;
        scoreText.text = "Score: " + score;
    }

    //不同分数对应评语
    public void Evaluate(int score)
    {
        if (score >= 90)
        {
            Comment = "非常棒！";
        }
        else if (score >= 80)
        {
            Comment = "很好！";
        }
        else
        {
            Comment = "遗憾失败！";
        }
    }
    // 显示总结面板
    public void ShowSummary()
    {
        Evaluate(score);
        summaryPanel.SetActive(true);
        summaryText.text = summary + "\n最终得分: " + score;
        CommentText.text = Comment;
    }

    public void OnButtonClicked1()
    {
        if (ToolPanel1 != null)
        {

            ToolPanel1.SetActive(false); // 设置Panel的Active属性为false来隐藏它

        }
    }

    public void OnButtonClicked2()
    {
        if (ToolPanel2 != null)
        {

            ToolPanel2.SetActive(false); // 设置Panel的Active属性为false来隐藏它

        }
    }

    public void OnButtonClicked3()
    {
        if (ToolPanel3 != null)
        {

            ToolPanel3.SetActive(false); // 设置Panel的Active属性为false来隐藏它

        }
    }
}

// 工具类型枚举
/*public enum ToolType
{
    None,
    Tool1,
    Tool2,
    CorrectTool
}
*/
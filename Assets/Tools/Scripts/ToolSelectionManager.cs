using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class ToolSelectionManager : MonoBehaviour
{
    
    public GameObject errorPanel; // ������ʾ���
    public Text errorText; // ������ʾ�ı�
    public Text scoreText; // �÷��ı�
    public GameObject summaryPanel; // �ܽ����
    public Text summaryText; // �ܽ��ı�
    public Text CommentText; // �����ı�

    public GameObject ToolPanel1;//����ѡ������
    public GameObject ToolPanel2;//����ѡ������
    public GameObject ToolPanel3;//����ѡ������
    public GameObject LanGan;//��������

    private bool isMovingErrorPanel = false;
    public Canvas canvas; // ����Canvas
    public float speed = 5f; // �����ٶ�
    public float maxYPosition = 30f; // ������ʾ�ﵽ�����Y����
    public AudioClip soundEffect2; // ��Ч
   // private AudioSource audioSource1; // ��ƵԴ
    public AudioSource audioSource2; // ��ƵԴ


    private int score = 100; // ��ʼ����
    private string summary = "�ɼ���¼:\n"; // �ܽ���Ϣ
    private string Comment ; // �ܽ���Ϣ
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
                // ����Panel��λ��
              //  RectTransform errorRectTransform = errorPanel.GetComponent<RectTransform>();
                errorRectTransform.anchoredPosition = new Vector2(0.4f, 0.5f); // ������Ҫ������ʼλ��
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))//����Ƿ���Q�����÷�����
        {
            LanGan.SetActive(true);
            isLanGanActive = true;
            timer = 0.0f; // ���ü�ʱ��

            
        }

        if (isLanGanActive)
        {
            timer += Time.deltaTime; // ���¼�ʱ��
            if (timer >= 2.0f) // �����ʱ���ﵽ2��
            {

                if (!audioSource2.isPlaying)
                {
                    audioSource2.Play();
                }
                isSoundActive = true;
                isLanGanActive = false; // ���ñ�־λ����ֹ�ظ�����
                timer = 0.0f; // ���ü�ʱ��
            }
        }

        if (isSoundActive) 
                {
            timer += Time.deltaTime; // ���¼�ʱ��
            if (timer >= 2.0f) // �����ʱ���ﵽ2��
            {
                ShowSummary();
                isSoundActive = false;// ���ñ�־λ����ֹ�ظ�����
                timer = 0.0f; // ���ü�ʱ��
            }
        }



    }

    // �ⲿ���õķ��������ڱ������
    public void ReportError(string error, int points)
    {
       
        // ���´�����ʾ�ı�
        errorText.text = error;
        // �۷�
        DeductPoints(points);
        // �����ܽ���Ϣ
        summary += error + ": -" + points + " points\n";
        // ��ʾ������ʾ���
        errorPanel.SetActive(true);
        isMovingErrorPanel = true; // ��ʼ�ƶ�������ʾ���
                                  
    }

    // �۷ַ���
    private void DeductPoints(int points)
    {
        score -= points;
        scoreText.text = "Score: " + score;
    }

    //��ͬ������Ӧ����
    public void Evaluate(int score)
    {
        if (score >= 90)
        {
            Comment = "�ǳ�����";
        }
        else if (score >= 80)
        {
            Comment = "�ܺã�";
        }
        else
        {
            Comment = "�ź�ʧ�ܣ�";
        }
    }
    // ��ʾ�ܽ����
    public void ShowSummary()
    {
        Evaluate(score);
        summaryPanel.SetActive(true);
        summaryText.text = summary + "\n���յ÷�: " + score;
        CommentText.text = Comment;
    }

    public void OnButtonClicked1()
    {
        if (ToolPanel1 != null)
        {

            ToolPanel1.SetActive(false); // ����Panel��Active����Ϊfalse��������

        }
    }

    public void OnButtonClicked2()
    {
        if (ToolPanel2 != null)
        {

            ToolPanel2.SetActive(false); // ����Panel��Active����Ϊfalse��������

        }
    }

    public void OnButtonClicked3()
    {
        if (ToolPanel3 != null)
        {

            ToolPanel3.SetActive(false); // ����Panel��Active����Ϊfalse��������

        }
    }
}

// ��������ö��
/*public enum ToolType
{
    None,
    Tool1,
    Tool2,
    CorrectTool
}
*/
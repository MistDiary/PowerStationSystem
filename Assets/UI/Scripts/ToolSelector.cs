using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ������д���


public class ToolSelector : MonoBehaviour
{
    // public WorkerController workerController;
    public Canvas canvas; // ����Canvas
    public float speed = 5f; // �����ٶ�
    public float maxYPosition = 30f; // ������ʾ�ﵽ�����Y����
    public GameObject[] toolModels; // �ĸ����ߵ�ģ��
    public Button[] toolButtons; // �ĸ����߰�ť
    public Text scoreText; // ��ʾ�÷ֵ�Text���
    public Text errorText; // ������ʾ��Text���
    public GameObject errorPanel; // ������ʾ�����
    public GameObject ToolPanel;//����ѡ������

    // private int score = 100; // ��ʼ����
    private int correctToolIndex = 2; // �����������������ȷ��
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
        // ��ʾ��Ӧ��������������
        foreach (GameObject tool in toolModels)
        {
            tool.SetActive(false);
        }
        toolModels[index].SetActive(true);

        // ����Ƿ�ѡ������ȷ�Ĺ���
        if (index == correctToolIndex)
        {
            // ��ȷѡ���߼�
            Debug.Log("Correct tool selected!");
        }
        else
        {
            Debug.Log("Wrong tool selected!");
            errorPanel.SetActive(true);
            isMovingErrorPanel = true; // ��ʼ�ƶ�������ʾ���
                                       // ����Panel��λ��
            RectTransform errorRectTransform = errorPanel.GetComponent<RectTransform>();
            errorRectTransform.anchoredPosition = new Vector2(0.4f, 0.5f); // ������Ҫ������ʼλ��

        }
    }
    public void OnButtonClicked()
    {
        if (ToolPanel != null)
        {
            
            ToolPanel.SetActive(false); // ����Panel��Active����Ϊfalse��������
           
        }
    }
}
    

  
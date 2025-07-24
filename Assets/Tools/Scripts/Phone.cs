using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ������д���
public class Phone : MonoBehaviour
{
    public int correctToolIndex = 2; // ��ȷ���ߵ�����
    public Button[] toolButtons; // �ĸ����߰�ť
                                 // public int toolType;
    public ToolSelectionManager toolSelectionManager;

    void Start()
    {
        // ����ToolSelectionManager�ǵ���ģʽ����ȫ�ַ���
        toolSelectionManager = FindObjectOfType<ToolSelectionManager>();
    }

    // ����������������õ����ѡ����һ������
    public void OnToolSelected(int index)
    {
       
        // ����Ƿ�ѡ������ȷ�Ĺ���
        if (index == correctToolIndex)
        {
            // ��ȷѡ���߼�
            Debug.Log("Correct tool selected!");
        }
        else
        {
            Debug.Log("Wrong tool selected!");
            // �������
            toolSelectionManager.ReportError("Incorrect Number selected", 10);

        }


    }
}
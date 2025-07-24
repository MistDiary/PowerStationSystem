using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using UnityEngine.UI; // ������д���





public class Tool : MonoBehaviour
{
    public int correctToolIndex = 2; // ��ȷ���ߵ�����
    public GameObject[] toolModels; // ����ģ������
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
        // ��ʾ��Ӧ��������������
        foreach (GameObject tool in toolModels)
        {
            tool.SetActive(false);
        }
        // ȷ��������Ч
        if (index >= 0 && index < toolModels.Length)
        {
            toolModels[index].SetActive(true);
        }

        // ����Ƿ�ѡ������ȷ�Ĺ���
        if (index == correctToolIndex)
        {
            // ��ȷѡ���߼�
            Debug.Log("Correct Tool selected!");
        }
        else
        {
            Debug.Log("Wrong Tool selected!");
            // �������
            toolSelectionManager.ReportError("ѡ�񹤾ߴ���", 10);

        }


    }
}
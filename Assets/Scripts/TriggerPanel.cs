using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class triggerPanel : MonoBehaviour
{
    public GameObject panel; // Ҫ��ʾ�����

    private void OnTriggerEnter(Collider other)
    {
        // ����ҽ��봥������ʱ��ʾ���\
        Debug.Log("����");
        if (other.CompareTag("player")) // ȷ��ֻ�д���"Player"��ǩ�Ķ����ܴ���
        {
            panel.SetActive(true); // ��ʾ���
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ������뿪��������ʱ�������
        if (other.CompareTag("player"))
        {
            panel.SetActive(false); // �������
        }
    }
}
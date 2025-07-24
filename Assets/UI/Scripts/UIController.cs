using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Camera playerCamera; // ��ҵ������
    public GameObject promptPanel; // ��ʾ����GameObject
    public float distanceFromCamera; // ������������ľ���
    public int triggerAreaID; // ���������Ψһ��ʶ��
    private bool isPanelActive = false; // ����Ƿ񼤻�ı�־
    public AudioSource audioSource;
    public AudioClip audioClip;
    // ��������ײ�����˴�����ʱ����
    private void OnTriggerEnter(Collider other)
    {
        if (!isPanelActive && other.CompareTag("player")) // ���������һ��"Player"��ǩ
        {
            ShowPromptPanel();
            audioSource.PlayOneShot(audioClip);
        }
    }

    // ��������ײ���뿪�˴�����ʱ����
    private void OnTriggerExit(Collider other)
    {
        if (isPanelActive && other.CompareTag("player"))
        {
            HidePromptPanel();
        }
    }

    void ShowPromptPanel()
    {
        // �������λ���������ǰ��ָ������
        promptPanel.transform.position = playerCamera.transform.position + playerCamera.transform.forward * distanceFromCamera;
        // ������峯�������
        promptPanel.transform.rotation = playerCamera.transform.rotation;
        // �������
        promptPanel.SetActive(true);
        isPanelActive = true;
    }

    void HidePromptPanel()
    {
        // �������
        promptPanel.SetActive(false);
        isPanelActive = false;
    }
    // �����ⲿ��������ʾ���ķ���
    public void DisplayPanelForArea(int areaID)
    {
        if (areaID == triggerAreaID)
        {
            ShowPromptPanel();
        }
    }

    public void HidePanelForArea(int areaID)
    {
        if (areaID == triggerAreaID)
        {
            HidePromptPanel();
        }
    }
}
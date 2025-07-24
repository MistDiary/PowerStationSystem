using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class HidePanel : MonoBehaviour
{
    public GameObject player; // ��Inspector�и�ֵΪ��Ҷ���
    public WorkerController workerController;
    public GameObject panelToHide; // ������Inspector����ק��ֵ��Panel����
    public ArrowDong arrowDong; // ����ArrowDong�ű�
    public Button soundButton; // ��һ����ť
    void Start()
    {
        workerController.enabled = false; // ����Ϸ��ʼʱ������ҿ���
        // ȷ����ʼʱ��ť�ǲ��ɼ���
        soundButton.gameObject.SetActive(false);
        if (arrowDong != null)
        {
            arrowDong.shouldDrawButtons = false;
        }
    }
    // ��ť���ʱ���ô˷���
    public void OnButtonClicked()
    {
        if (panelToHide != null)
        {
            Debug.Log("StartGame button clicked"); // ��ӵ�����־
            panelToHide.SetActive(false); // ����Panel��Active����Ϊfalse��������
   
            workerController.enabled = true; // ������ҿ���
            soundButton.gameObject.SetActive(true);
            // ��ʾ·����ť
            if (arrowDong != null)
            {
                arrowDong.shouldDrawButtons = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TIAOYUE : MonoBehaviour
{

    public GameObject StartUI; // ��ʼ���
    public GameObject QuestionUIORDER; // �������                          // Start is called before the first frame update

    public void OnButtonClicked()
    {
        if (StartUI != null)
        {

            StartUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
            QuestionUIORDER.SetActive(true);
        }
    }
}

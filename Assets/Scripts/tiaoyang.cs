using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tiaoyang : MonoBehaviour
{

    public GameObject StartUI; // ��ʼ���
    public GameObject QuestionUIRANDOM; // �������                          // Start is called before the first frame update

    public void OnButtonClicked()
    {
        if (StartUI != null)
        {

            StartUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
            QuestionUIRANDOM.SetActive(true);
        }
    }
}

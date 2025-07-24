using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tiaoyang : MonoBehaviour
{

    public GameObject StartUI; // 开始面板
    public GameObject QuestionUIRANDOM; // 答题面板                          // Start is called before the first frame update

    public void OnButtonClicked()
    {
        if (StartUI != null)
        {

            StartUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它
            QuestionUIRANDOM.SetActive(true);
        }
    }
}

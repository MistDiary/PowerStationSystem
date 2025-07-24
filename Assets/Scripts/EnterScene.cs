using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    public GameObject SuccessUI; // 成功面板
    public void EnterSceneButton()
    {

       
       SuccessUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它


  }
}

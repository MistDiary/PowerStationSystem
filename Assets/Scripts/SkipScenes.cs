using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void EnterTeachingMode()
    {

        SceneManager.LoadScene(1);


    }
    public void EnterScoreMode()
    {

        SceneManager.LoadScene(2);


    }
    public void EnterGameMode()
    {

        SceneManager.LoadScene(0);


    }


    public void EnterHome()
    {

        SceneManager.LoadScene(3);


    }
    public void QuitApplication()
    {
        // �ڱ༭����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // �ڱ�����Ӧ�ó�����
#else
        Application.Quit();
#endif
    }
}

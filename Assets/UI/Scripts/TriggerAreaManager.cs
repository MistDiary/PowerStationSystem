using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaManager : MonoBehaviour
{
    public UIController[] promptPanels; // ���д��������������������

    // ����ҽ���ĳ����������ʱ���ô˷���
    public void PlayerEnteredArea(int areaID)
    {
        foreach (var panelController in promptPanels)
        {
            panelController.DisplayPanelForArea(areaID);
        }
    }


   public void HideUIById(int areaID)
    {

    }
}
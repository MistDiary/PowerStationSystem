using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaManager : MonoBehaviour
{
    public UIController[] promptPanels; // 所有触发区域的面板控制器数组

    // 当玩家进入某个触发区域时调用此方法
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
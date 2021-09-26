using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitClick : MonoBehaviour
{
    public void EnableWheel()
    {
        if (!SceneConstants.InDialouge && !SceneConstants.InSelection)
        {
            SceneConstants.InSelection = true;
            SceneConstants.SelectionWheel.SetActive(true);

            SceneConstants.SelectionWheel.GetComponent<MenuScript>().selection = 0;
            SceneConstants.SelectionWheel.GetComponent<MenuScript>().menuItems[0].GetComponent<MenuItemScript>().Select();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveItem : MonoBehaviour
{
    public void Give()
    {
        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "DialogueText")
            {
                text.text = "** " + SceneConstants.currAttr.Item + " has been given to " + SceneConstants.otherAttr.CharName + " **";
                break;
            }
        }

        SceneConstants.otherAttr.Item = SceneConstants.currAttr.Item;
        SceneConstants.currAttr.Item = "";

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name == "GiveButton")
            {
                button.interactable = false;
                break;
            }
        }

        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "CharacterItem")
            {
                text.text = "Item:";
                break;
            }     
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveItem : MonoBehaviour
{
    public void Give()
    {
        playerAttributes otherAttr = SceneConstants.DiaCharacter.GetComponent<playerAttributes>();

        GameObject curr = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (player.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
            {
                curr = player;
                break;
            }
        }

        playerAttributes currAttr = curr.GetComponent<playerAttributes>();

        otherAttr.Item = currAttr.Item;
        currAttr.Item = "";

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
                text.text = "Item:";
        }
    }
}

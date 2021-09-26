using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    void Start()
    {
        SceneConstants.Possessable = GetSceneCharacters();
        SceneConstants.currentPossession = 0;

        SetupUI();

        SceneConstants.InDialouge = false;
        SceneConstants.InConversation = false;
        SceneConstants.otherAttr = null;
    }

    //Gets names of all game objecst that are characters (have the "Character" tag)
    private List<string> GetSceneCharacters()
    {
        bool first = false;
        List<string> temp = new List<string>();
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (c.GetComponent<playerAttributes>().IsConverted)
            {
                temp.Add(c.GetComponent<playerAttributes>().CharName);
                if (!first)
                {
                    first = true;
                    SceneConstants.currAttr = c.GetComponent<playerAttributes>();
                }
            }
        }

        return temp;
    }

    //Setup necessary Player and Dialogue UI elements
    private void SetupUI()
    {
        SceneConstants.SceneDiaUI = GameObject.Find("DialogueUI");
        SceneConstants.PlayerUI = GameObject.Find("PlayerUI");

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name != "TalkButton")
                button.interactable = false;
        }
        SceneConstants.conversionBar = new List<Image>();
        foreach (Image bar in SceneConstants.SceneDiaUI.GetComponentsInChildren<Image>())
        {
            if (bar.name.Contains("conversion"))
            {
                SceneConstants.conversionBar.Add(bar);
            }
        }
        SceneConstants.SceneDiaUI.SetActive(false);

        foreach (TextMeshProUGUI text in SceneConstants.PlayerUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "PlayerName")
                text.text = SceneConstants.currAttr.CharName;
            if (text.name == "PlayerItem")
                text.text = "Item: " + SceneConstants.currAttr.Item;
        }
        foreach (Image portrait in SceneConstants.PlayerUI.GetComponentsInChildren<Image>())
        {
            if (portrait.name == "PlayerPortrait")
            {
                portrait.sprite = SceneConstants.currAttr.Portrait;
                break;
            }
        }
    }
}

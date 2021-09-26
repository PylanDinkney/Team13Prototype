using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    void Start()
    {
        GetSceneCharacters();
        SceneConstants.currentPossession = "Summoner";
        SceneConstants.currAttr = SceneConstants.Possessable[SceneConstants.currentPossession].GetComponent<playerAttributes>();

        SetupUI();

        SceneConstants.InDialouge = false;
        SceneConstants.InConversation = false;
        SceneConstants.otherAttr = null;
    }

    //Gets names of all game objecst that are characters (have the "Character" tag)
    private void GetSceneCharacters()
    {
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (c.GetComponent<playerAttributes>().IsConverted)
                SceneConstants.Possessable.Add(c.GetComponent<playerAttributes>().CharName, c);
        }
    }

    //Setup necessary Player and Dialogue UI elements
    private void SetupUI()
    {
        SceneConstants.SceneDiaUI = GameObject.Find("DialogueUI");
        SceneConstants.PlayerUI = GameObject.Find("PlayerUI");
        SceneConstants.SelectionWheel = GameObject.Find("PieMenu");
        SceneConstants.SelectionWheel.SetActive(false);

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

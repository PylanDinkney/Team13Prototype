using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    void Start()
    {
        SceneConstants.Possessable = GetSceneCharacters();
        SceneConstants.currentPossession = 0;
        SceneConstants.SceneDiaUI = GameObject.Find("DialogueUI");
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
        SceneConstants.InDialouge = false;
        SceneConstants.InConversation = false;
        SceneConstants.otherAttr = null;
    }

    //Gets names of all game objecst that are characters (have the "Character" tag)
    List<string> GetSceneCharacters()
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SceneConstants.SceneDiaUI.SetActive(false);
        SceneConstants.InDialouge = false;
        SceneConstants.InConversation = false;
        SceneConstants.DiaCharacter = null;
    }

    //Gets names of all game objecst that are characters (have the "Character" tag)
    List<string> GetSceneCharacters()
    {
        List<string> temp = new List<string>();
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Character"))
            temp.Add(c.GetComponent<playerAttributes>().CharName);
        return temp;
    }
}

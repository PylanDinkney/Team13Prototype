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
        SceneConstants.SceneDiaUI.SetActive(false);
        SceneConstants.InDialouge = false;
    }

    //Gets names of all game objecst that are characters (have the "Character" tag)
    List<string> GetSceneCharacters()
    {
        List<string> temp = new List<string>();
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Character"))
            temp.Add(c.name);
        return temp;
    }
}

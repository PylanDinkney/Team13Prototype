using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertCharacter : MonoBehaviour
{
    public void ConvertPlayer()
    {
        playerAttributes otherAttr = SceneConstants.DiaCharacter.GetComponent<playerAttributes>();

        GameObject curr = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (player.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
                curr = player;
        }

        playerAttributes currAttr = SceneConstants.DiaCharacter.GetComponent<playerAttributes>();

        otherAttr.IsConverted = true;
        SceneConstants.Possessable.Add(otherAttr.CharName);

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name == "ConvertButton")
                button.interactable = false;
            else if (button.name == "GiveButton")
            {
                if (otherAttr.IsConverted && currAttr.Item != "" && otherAttr.Item == "")
                    button.interactable = true;
            }
        }

        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "DialogueText")
            {
                text.text = "** " + otherAttr.CharName + " has been converted and is now controllable **";
                break;
            }
        }

        string path = path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
            SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + otherAttr.CharName + "/Post"; ;

        SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        DialogueChannel c = (DialogueChannel)Resources.Load("Dialogue/DialogueChannel");
        SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue((Dialogue)Resources.Load(path)); });
    }
}

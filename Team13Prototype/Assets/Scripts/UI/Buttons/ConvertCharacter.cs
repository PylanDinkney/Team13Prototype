using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertCharacter : MonoBehaviour
{
    public void ConvertPlayer()
    {
        SceneConstants.otherAttr.IsConverted = true;
        SceneConstants.Possessable.Add(SceneConstants.otherAttr.CharName);

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name == "ConvertButton")
                button.interactable = false;
            else if (button.name == "GiveButton")
            {
                if (SceneConstants.otherAttr.IsConverted && SceneConstants.currAttr.Item != "" && SceneConstants.otherAttr.Item == "")
                    button.interactable = true;    
            }
        }

        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "DialogueText")
            {
                text.text = "** " + SceneConstants.otherAttr.CharName + " has been converted and is now controllable **";
                break;
            }
        }

        string path = path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
            SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + SceneConstants.otherAttr.CharName + "/Post"; ;

        SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        DialogueChannel c = (DialogueChannel)Resources.Load("Dialogue/DialogueChannel");
        SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue((Dialogue)Resources.Load(path)); });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConvertCharacter : MonoBehaviour
{
    public void ConvertPlayer()
    {
        SceneConstants.otherAttr.IsConverted = true;
        SceneConstants.Possessable.Add(SceneConstants.otherAttr.CharName, SceneConstants.otherAttr.gameObject);

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name == "ConvertButton")
                button.interactable = false;
            else if (button.name == "GiveButton")
            {
                if (SceneConstants.otherAttr.IsConverted && (SceneConstants.currAttr.Item != null && SceneConstants.currAttr.Item != "")
                    && (SceneConstants.otherAttr.Item == null || SceneConstants.otherAttr.Item == ""))
                    button.interactable = true;    
            }
        }

        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "DialogueText")
            {
                text.text = "** " + SceneConstants.otherAttr.CharName + " has been converted and is now controllable. You can select the character portrait to possess a different character. **";
                break;
            }
        }

        Sprite converted = Resources.Load<Sprite>("UIAssets/light3");
        for (int i = 0; i < 6; i++)
        {
            if (i < SceneConstants.otherAttr.ConversionThreshold)//if more unfilled conversion bars needed add one
            {
                SceneConstants.conversionBar[i].enabled = true;
                SceneConstants.conversionBar[i].sprite = converted;
            }
            else
            {//make sure the rest are invisible
                SceneConstants.conversionBar[i].enabled = false;
            }

        }

        AddDialogue();
        UpdateWheel();
    }

    private void AddDialogue()
    {
        string path = "Dialogue/" + SceneConstants.currentPossession + "/" +
            SceneConstants.currentPossession + "_" + SceneConstants.otherAttr.CharName + "/Post";

        DialogueChannel c = (DialogueChannel)Resources.Load("Dialogue/DialogueChannel");
        Dialogue d = (Dialogue)Resources.Load(path);
        SceneConstants.SceneDiaUI.GetComponentInChildren<Button>().onClick.RemoveAllListeners();

        Button talk = null;
        foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
        {
            if (button.name == "TalkButton")
                talk = button;
        }

        if (d == null)
            talk.interactable = false;
        else
        {
            SceneConstants.SceneDiaUI.GetComponentInChildren<Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue(d); });
            talk.interactable = true;
        }
    }

    private void UpdateWheel()
    {
        GameObject[] objects = SceneConstants.SelectionWheel.GetComponent<MenuScript>().menuItems;
        foreach (GameObject item in objects)
        {
            MenuItemScript ms = item.GetComponent<MenuItemScript>();
            if (ms.charName == SceneConstants.otherAttr.CharName)
            {
                ms.itemText.text = ms.charName;
                ms.isActive = true;
            }
        }
        SceneConstants.SelectionWheel.GetComponent<MenuScript>();
        string tempName = SceneConstants.otherAttr.CharName;
    }
}

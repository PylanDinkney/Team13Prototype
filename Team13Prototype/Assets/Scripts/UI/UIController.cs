using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    void Update()
    {
        if (!SceneConstants.InDialouge && Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
        {
            // Get closest valid interactable character
            Collider[] objects = Physics.OverlapSphere(transform.position, 2);

            float dist = 0;
            GameObject other = null;
            foreach (Collider c in objects)
            {
                if (c.tag == "Character" && c.gameObject.GetComponent<playerAttributes>().CharName != SceneConstants.Possessable[SceneConstants.currentPossession])
                {
                    if (other == null)
                    {
                        dist = Vector3.Distance(this.transform.position, c.transform.position);
                        other = c.gameObject;
                    }
                    else
                    {
                        float temp = Vector3.Distance(this.transform.position, c.transform.position);
                        if (temp < dist)
                        {
                            dist = temp;
                            other = c.gameObject;
                        }
                    }
                }
            }

            // Open UI and setup/disable nescessary buttons including loading specific dialogue tree from resources
            if (other != null)
            {
                // Find path to dialogue and add button listener for Dialogue Channel request to start dialogue with found dialogue
                SceneConstants.InDialouge = true;
                SceneConstants.otherAttr = other.GetComponent<playerAttributes>();

                AddDialogue();
                EnableButtons();
                SetText();
                SetPortrait();
                DrawConversionBar();

                SceneConstants.SceneDiaUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneConstants.InDialouge && !SceneConstants.InConversation)
        {
            SceneConstants.InDialouge = false;
            SceneConstants.otherAttr = null;

            
            foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
            {
                if (button.name != "TalkButton")
                    button.interactable = false;
            }
            foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.name == "DialogueText")
                {
                    text.text = "";
                    break;
                }   
            }

            SceneConstants.SceneDiaUI.SetActive(false);
        }
    }

    private void AddDialogue()
    {
        string path = "";
        if (SceneConstants.otherAttr.IsConverted)
        {
            path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
            SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + SceneConstants.otherAttr.CharName + "/Post";
        }
        else
        {
            path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
            SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + SceneConstants.otherAttr.CharName + "/Pre";
        }

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

    private void EnableButtons()
    {
        foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
        {
            if (button.name == "ConvertButton")
            {
                if (!SceneConstants.otherAttr.IsConverted)
                {
                    int level = 0;
                    if (SceneConstants.currAttr.Item != "")
                    {
                        if (SceneConstants.currAttr.Item == SceneConstants.otherAttr.ItemWeakness)
                            level += 2;
                    }
                    if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.Trait)
                        level += 1;
                    else if (SceneConstants.currAttr.TraitWeakness == SceneConstants.otherAttr.Trait)
                        level -= 1;
                    else if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.TraitWeakness)
                        level += 2;
                    if (level >= SceneConstants.otherAttr.ConversionThreshold)
                        button.interactable = true;
                }

            }
            if (button.name == "GiveButton")
            {
                if (SceneConstants.otherAttr.IsConverted && (SceneConstants.currAttr.Item != null && SceneConstants.currAttr.Item != "")
                    && (SceneConstants.otherAttr.Item == null || SceneConstants.otherAttr.Item == ""))
                    button.interactable = true;
            }
        }
    }

    private void SetText()
    {
        // Change Text components of UI
        foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "DialogueText")
                text.text = SceneConstants.otherAttr.Greeting;
            if (text.name == "CharacterName")
                text.text = SceneConstants.otherAttr.CharName;
            if (text.name == "CharacterTrait")
                text.text = SceneConstants.otherAttr.Trait;
        }
        foreach (TextMeshProUGUI text in SceneConstants.PlayerUI.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name == "PlayerItem")
            {
                text.text = "Item: " + SceneConstants.currAttr.Item;
                break;
            }
        }
    }

    private void SetPortrait()
    {
        foreach (Image portrait in SceneConstants.SceneDiaUI.GetComponentsInChildren<Image>())
        {
            if (portrait.name == "DialoguePortrait")
            {
                portrait.sprite = SceneConstants.otherAttr.Portrait;
                break;
            }
        }
    }

    private void DrawConversionBar() {
        Sprite on = Resources.Load<Sprite>("UIAssets/light2");
        Sprite off = Resources.Load<Sprite>("UIAssets/light");
        Sprite converted = Resources.Load<Sprite>("UIAssets/light3");

        int level = 0;
        if (SceneConstants.currAttr.Item != "")
        {
            if (SceneConstants.currAttr.Item == SceneConstants.otherAttr.ItemWeakness)
                level += 2;
        }
        if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.Trait)
            level += 1;
        else if (SceneConstants.currAttr.TraitWeakness == SceneConstants.otherAttr.Trait)
            level -= 1;
        else if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.TraitWeakness)
            level += 2;

        if (level >= SceneConstants.otherAttr.ConversionThreshold)
            level = SceneConstants.otherAttr.ConversionThreshold;

        int unfilled = SceneConstants.otherAttr.ConversionThreshold - level;

        if (!SceneConstants.otherAttr.IsConverted)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i < level)//if more filled conversion bars needed add one
                {
                    SceneConstants.conversionBar[i].enabled = true;
                    SceneConstants.conversionBar[i].sprite = on;
                }
                else if (i < level + unfilled)//if more unfilled conversion bars needed add one
                {
                    SceneConstants.conversionBar[i].enabled = true;
                    SceneConstants.conversionBar[i].sprite = off;
                }
                else
                {//make sure the rest are invisible
                    SceneConstants.conversionBar[i].enabled = false;
                }

            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (i < level + unfilled)//if more unfilled conversion bars needed add one
                {
                    SceneConstants.conversionBar[i].enabled = true;
                    SceneConstants.conversionBar[i].sprite = converted;
                }
                else
                {//make sure the rest are invisible
                    SceneConstants.conversionBar[i].enabled = false;
                }

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
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
                SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue((Dialogue)Resources.Load(path)); });

                // Enable buttons that can be enabled (convert, lure, give)
                foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
                {
                    if (button.name == "ConvertButton")
                    {
                        if (!SceneConstants.otherAttr.IsConverted)
                        {
                            int level = 0;
                            if (SceneConstants.currAttr.Item == SceneConstants.otherAttr.ItemWeakness)
                                level += 2;
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
                        if (SceneConstants.otherAttr.IsConverted && SceneConstants.currAttr.Item != "" && SceneConstants.otherAttr.Item == "")
                            button.interactable = true;
                    }
                }

                // Change Text components of UI
                foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "DialogueText")
                        text.text = SceneConstants.otherAttr.Greeting;
                    if (text.name == "CharacterName")
                        text.text = SceneConstants.otherAttr.CharName;
                    if (text.name == "CharacterTrait")
                        text.text = SceneConstants.otherAttr.Trait;
                    if (text.name == "CharacterItem")
                        text.text = "Item: " + SceneConstants.currAttr.Item;
                }

                SceneConstants.SceneDiaUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneConstants.InDialouge && !SceneConstants.InConversation)
        {
            SceneConstants.InDialouge = false;
            SceneConstants.otherAttr = null;

            
            foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
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
}

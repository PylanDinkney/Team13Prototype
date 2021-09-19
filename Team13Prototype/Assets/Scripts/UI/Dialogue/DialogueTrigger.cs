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
                SceneConstants.DiaCharacter = other;

                playerAttributes otherAttr = other.GetComponent<playerAttributes>();

                GameObject curr = null;
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Character"))
                {
                    if (player.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
                        curr = player;
                }
                playerAttributes currAttr = curr.GetComponent<playerAttributes>();

                string path = "";
                if (otherAttr.IsConverted)
                {
                    path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
                    SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + otherAttr.CharName + "/Post";
                }
                else
                {
                    path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
                    SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + otherAttr.CharName + "/Pre";
                }

                DialogueChannel c = (DialogueChannel)Resources.Load("Dialogue/DialogueChannel");
                SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue((Dialogue)Resources.Load(path)); });

                // Enable buttons that can be enabled (convert, lure, give)
                foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
                {
                    if (button.name == "ConvertButton")
                    {
                        if (!otherAttr.IsConverted)
                        {
                            int level = 0;
                            if (currAttr.Item == otherAttr.ItemWeakness)
                                level += 2;
                            if (currAttr.Trait == otherAttr.Trait)
                                level += 1;
                            else if (currAttr.TraitWeakness == otherAttr.Trait)
                                level -= 1;
                            else if (currAttr.Trait == otherAttr.TraitWeakness)
                                level += 2;

                            if (level >= otherAttr.ConversionThreshold)
                                button.interactable = true;
                        }
                        
                    }
                    if (button.name == "GiveButton")
                    {
                        if (otherAttr.IsConverted && currAttr.Item != "" && otherAttr.Item == "")
                            button.interactable = true;
                    }
                }

                // Change Text components of UI
                foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "DialogueText")
                        text.text = otherAttr.Greeting;
                    if (text.name == "CharacterName")
                        text.text = otherAttr.CharName;
                    if (text.name == "CharacterTrait")
                        text.text = otherAttr.Trait;
                    if (text.name == "CharacterItem")
                        text.text = "Item: " + currAttr.Item;
                }

                SceneConstants.SceneDiaUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneConstants.InDialouge && !SceneConstants.InConversation)
        {
            SceneConstants.InDialouge = false;
            SceneConstants.DiaCharacter = null;

            
            foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
            {
                if (button.name != "TalkButton")
                    button.interactable = false;
            }
            foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.name == "DialogueText")
                    text.text = "";
            }

            SceneConstants.SceneDiaUI.SetActive(false);
        }
    }
}

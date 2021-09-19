using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{

    void Update()
    {
        if (!SceneConstants.InDialouge && Input.GetKeyDown(KeyCode.F) && this.gameObject.name == SceneConstants.Possessable[SceneConstants.currentPossession])
        {
            // Get closest valid interactable character
            Collider[] objects = Physics.OverlapSphere(transform.position, 2);

            float dist = 0;
            GameObject curr = null;
            foreach (Collider c in objects)
            {
                if (c.tag == "Character" && c.gameObject.name != SceneConstants.Possessable[SceneConstants.currentPossession])
                {
                    if (curr == null)
                    {
                        dist = Vector3.Distance(this.transform.position, c.transform.position);
                        curr = c.gameObject;
                    }
                    else
                    {
                        float temp = Vector3.Distance(this.transform.position, c.transform.position);
                        if (temp < dist)
                        {
                            dist = temp;
                            curr = c.gameObject;
                        }
                    }
                }
            }

            // Open UI and setup/disable nescessary buttons including loading specific dialogue tree from resources
            if (curr != null)
            {
                // Find path to dialogue and add button listener for Dialogue Channel request to start dialogue with found dialogue
                SceneConstants.InDialouge = true;
                SceneConstants.DiaCharacter = curr;

                string path = "";
                if (curr.GetComponent<playerAttributes>().IsConverted)
                {
                    path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
                    SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + curr.name + "/Post";
                }
                else
                {
                    path = "Dialogue/" + SceneConstants.Possessable[SceneConstants.currentPossession] + "/" +
                    SceneConstants.Possessable[SceneConstants.currentPossession] + "_" + curr.name + "/Pre";
                }

                Debug.Log(path);
                DialogueChannel c = (DialogueChannel)Resources.Load("Dialogue/DialogueChannel");
                SceneConstants.SceneDiaUI.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { c.RaiseRequestDialogue((Dialogue)Resources.Load(path)); });

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

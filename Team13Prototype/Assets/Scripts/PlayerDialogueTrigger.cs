using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{

    void Update()
    {
        if (!SceneConstants.InDialouge && Input.GetKeyDown(KeyCode.F) && this.gameObject.name == SceneConstants.Possessable[SceneConstants.currentPossession])
        {
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

            if (curr != null)
            {
                SceneConstants.InDialouge = true;
                SceneConstants.SceneDiaUI.SetActive(true);
                Dialouge test = GameObject.Find("DialogueUI").GetComponent<Dialouge>();
                test.NextDialogue(this.gameObject.name + " started a conversation with " + curr.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneConstants.InDialouge)
        {
            SceneConstants.SceneDiaUI.SetActive(false);
            SceneConstants.InDialouge = false;
        }
    }
}

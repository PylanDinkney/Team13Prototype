using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameConstants.Possessable = GetSceneCharacters();
        GameConstants.currentPossession = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != GameConstants.Possessable[GameConstants.currentPossession] && other.gameObject.tag == "Character")
        {
            Debug.Log("Testing");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != GameConstants.Possessable[GameConstants.currentPossession] && other.gameObject.tag == "Character")
        {
            Debug.Log("Testing");
        }
    }

    List<string> GetSceneCharacters()
    {
        List<string> temp = new List<string>();
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Character"))
            temp.Add(c.name);
        return temp;
    }
}

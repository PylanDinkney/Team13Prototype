using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public string item = "emptyItem";
    private bool inRange;

    void Start(){
        inRange = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C) && inRange == true){
            Debug.Log("Picked up item!");
            GameObject player = GameObject.Find(SceneConstants.Possessable[SceneConstants.currentPossession]);
            playerAttributes playerAttributes = player.gameObject.GetComponent<playerAttributes>();
            playerAttributes.Item = item;
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == SceneConstants.Possessable[SceneConstants.currentPossession]){
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == SceneConstants.Possessable[SceneConstants.currentPossession]){
            inRange = false;
        }
    }
}

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
            SceneConstants.currAttr.Item = item;
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
            inRange = true;
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
            inRange = false;
    }
}

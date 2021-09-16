using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public string item = "emptyItem";

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == GameConstants.Possessable[GameConstants.currentPossession] && Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Picked up item!");
            playerAttributes playerAttributes = other.gameObject.GetComponent<playerAttributes>();
            playerAttributes.item = item;
        }
    }
}

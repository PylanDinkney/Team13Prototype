using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name != GameConstants.Possessable[GameConstants.currentPossession]) {
            
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name != GameConstants.Possessable[GameConstants.currentPossession]) {
            GameConstants.interactibleInRange = "";
        }
    }
}

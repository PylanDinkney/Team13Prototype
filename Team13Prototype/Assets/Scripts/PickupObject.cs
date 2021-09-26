using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public string item = "emptyItem";
    private bool inRange;
    public AudioSource pickupItemSound;

    void Start(){
        GameObject tempSound = GameObject.Find("ItemSound");
        pickupItemSound = tempSound.GetComponent<AudioSource>();
        inRange = false;
        pickupItemSound.Stop();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.C) && inRange == true){
            pickupItemSound.Play();
            SceneConstants.currAttr.Item = item;
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.currentPossession)
            inRange = true;
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.currentPossession)
            inRange = false;
    }
}

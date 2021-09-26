using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour
{
    public string keyRequired = "Office Key";
    private bool openSesame = false;
    GameObject door;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character" && other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.currentPossession && other.gameObject.GetComponent<playerAttributes>().Item == keyRequired){
            openSesame = true;
        }
    }

    void Start(){
        door = GameObject.Find("Doorway");
    }

    void Update(){
        
        if(openSesame == true && door.transform.position.x < 1){
            door.transform.Translate(0.05f, 0, 0);
        }
    }
}

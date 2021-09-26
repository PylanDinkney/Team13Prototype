using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchRooms : MonoBehaviour
{
    public string roomToGoTo = "Basementwhitebox";

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character" && other.gameObject.GetComponent<playerAttributes>().CharName == SceneConstants.currentPossession){
            SceneManager.LoadScene(roomToGoTo);
        }
    }
}

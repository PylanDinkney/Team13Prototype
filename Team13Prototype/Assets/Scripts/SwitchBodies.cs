using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBodies : MonoBehaviour
{
    private static bool keyIsDown;
    public string SwitchBodiesKey = "e";

    void Start(){
        keyIsDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(SwitchBodiesKey) && keyIsDown == false){
            keyIsDown = true;
            if(GameConstants.currentPossession == "StartingPossession"){
                GameConstants.currentPossession = "SecondaryPossession";
            } else {
                GameConstants.currentPossession = "StartingPossession";
            }
        } else if(Input.GetKeyUp(SwitchBodiesKey) && keyIsDown == true){
            keyIsDown = false;
        }
    }
}

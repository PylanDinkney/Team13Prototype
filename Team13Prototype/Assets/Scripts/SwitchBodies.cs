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
        if(Input.GetKeyDown(SwitchBodiesKey) && keyIsDown == false)
        {
            keyIsDown = true;
            if(GameConstants.Possessable.Count >= 2)
            {
                GameConstants.currentPossession++;
                if (GameConstants.currentPossession >= GameConstants.Possessable.Count) {
                    GameConstants.currentPossession = 0;
                }
            }
        } else if(Input.GetKeyUp(SwitchBodiesKey) && keyIsDown == true){
            keyIsDown = false;
        }
    }
}

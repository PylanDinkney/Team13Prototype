using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBodies : MonoBehaviour
{
    private static bool keyIsDown;
    private static bool eWentDown;
    private static bool qWentDown;
    public string SwitchBodiesKey = "e";
    public string SwitchBodiesKeyBack = "q";

    void Start(){
        keyIsDown = false;
        eWentDown = false;
        qWentDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(SwitchBodiesKey) || Input.GetKeyDown(SwitchBodiesKeyBack)) && keyIsDown == false)
        {
            keyIsDown = true;
            if(GameConstants.Possessable.Count >= 2)
            {
                if(Input.GetKeyDown(SwitchBodiesKey)){
                    GameConstants.currentPossession++;
                    eWentDown = true;
                }
                else{
                    GameConstants.currentPossession--;
                    qWentDown = true;
                }
                if (GameConstants.currentPossession >= GameConstants.Possessable.Count) {
                    GameConstants.currentPossession = 0;
                } else if(GameConstants.currentPossession < 0){
                    GameConstants.currentPossession = GameConstants.Possessable.Count - 1;
                }
            }
        } else if(Input.GetKeyUp(SwitchBodiesKey) && keyIsDown == true && eWentDown == true){
            keyIsDown = false;
            eWentDown = false;
        } else if(Input.GetKeyUp(SwitchBodiesKeyBack) && keyIsDown == true && qWentDown == true){
            keyIsDown = false;
            qWentDown = false;
        }
    }
}

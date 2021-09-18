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
        if (!SceneConstants.InDialouge)
        {
            if ((Input.GetKeyDown(SwitchBodiesKey) || Input.GetKeyDown(SwitchBodiesKeyBack)) && keyIsDown == false)
            {
                keyIsDown = true;
                if (SceneConstants.Possessable.Count >= 2) //if there is a body to switch to
                {
                    if (Input.GetKeyDown(SwitchBodiesKey)) //switch to next body
                    {
                        SceneConstants.currentPossession++;
                        eWentDown = true;
                    }
                    else//switch to previous body
                    {
                        SceneConstants.currentPossession--;
                        qWentDown = true;
                    }
                    if (SceneConstants.currentPossession >= SceneConstants.Possessable.Count)//if index(currentPossession) is out of bounds, wrap around
                        SceneConstants.currentPossession = 0;
                    else if (SceneConstants.currentPossession < 0)
                        SceneConstants.currentPossession = SceneConstants.Possessable.Count - 1;
                }
            }//key up, reset boolean flags
            else if (Input.GetKeyUp(SwitchBodiesKey) && keyIsDown == true && eWentDown == true)
            {
                keyIsDown = false;
                eWentDown = false;
            }
            else if (Input.GetKeyUp(SwitchBodiesKeyBack) && keyIsDown == true && qWentDown == true)
            {
                keyIsDown = false;
                qWentDown = false;
            }
        }
    }
}

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
                if (SceneConstants.Possessable.Count >= 2)
                {
                    if (Input.GetKeyDown(SwitchBodiesKey))
                    {
                        SceneConstants.currentPossession++;
                        eWentDown = true;
                    }
                    else
                    {
                        SceneConstants.currentPossession--;
                        qWentDown = true;
                    }
                    if (SceneConstants.currentPossession >= SceneConstants.Possessable.Count)
                        SceneConstants.currentPossession = 0;
                    else if (SceneConstants.currentPossession < 0)
                        SceneConstants.currentPossession = SceneConstants.Possessable.Count - 1;
                }
            }
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

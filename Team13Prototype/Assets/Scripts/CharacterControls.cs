using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    // Switching Bodies
    private static bool keyIsDown;
    private static bool eWentDown;
    private static bool qWentDown;
    public string SwitchBodiesKey = "e";
    public string SwitchBodiesKeyBack = "q";

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        keyIsDown = false;
        eWentDown = false;
        qWentDown = false;
    }

    void Update()
    {
        if (!SceneConstants.InDialouge)
        {
            //Movement
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (SceneConstants.Possessable[SceneConstants.currentPossession] == gameObject.GetComponent<playerAttributes>().CharName)
            {
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * playerSpeed);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            //Switching Bodies
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
        else
        {
            //Movement
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}

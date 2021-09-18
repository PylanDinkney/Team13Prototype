using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        if (!SceneConstants.InDialouge)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (SceneConstants.Possessable[SceneConstants.currentPossession] == gameObject.name)
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
        }
        else
        {
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

using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour 
{
    [HideInInspector]
    public PlayerMovement playerMovement;
    [HideInInspector]
    public PlayerInput playerInput;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.InputEnabled = true;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (playerMovement.isMoving) return;

        playerInput.GetKeyInput();

        if (playerInput.V == 0)
        {
            if (playerInput.H < 0)
            {
                playerMovement.MoveLeft();
            }
            else if (playerInput.H > 0)
            {
                playerMovement.MoveRight();
            }
        }
        else if (playerInput.H == 0)
        {
            if (playerInput.V < 0)
            {
                playerMovement.MoveBackward();
            }
            else if (playerInput.V > 0)
            {
                playerMovement.MoveForward();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    public CharacterController controller;
    private Vector3 playerVelocity;

    public Vector2 move;
    [SerializeField] private float playerSpeed = 2.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        move = new Vector2(movementInput.x, movementInput.y);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        controller.Move(move * (Time.deltaTime * playerSpeed));

        if(move != Vector2.zero)
        {
            gameObject.transform.up = move;
        }
    }
}

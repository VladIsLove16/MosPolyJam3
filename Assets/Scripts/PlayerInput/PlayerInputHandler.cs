using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } // Экспонируем направление движения для других компонентов

    private PlayerInputActions playerInput;
    public Inventory inventory;
    Player Player;

    private Dictionary<InputAction, InventoryItemType> itemMappings = new();
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        inventory = ServiceLocator.Current.Get<Inventory>();
        Player = ServiceLocator .Current.Get<Player>();

        itemMappings = new Dictionary<InputAction, InventoryItemType>//здесь есть небольшая затычка с тем, что по первой кнопке
                                                                     //должен использоватся предмет, который лежит в первом слоте UI
        {
            { playerInput.Player.UseItem1, InventoryItemType.cottons },
            { playerInput.Player.UseItem2, InventoryItemType.milk },
            { playerInput.Player.UseItem3, InventoryItemType.rat }
        };
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;
        foreach (var action in itemMappings.Keys)
        {
            action.performed += UseItem;
        }
        playerInput.Player.Shoot.performed += Shoot ;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Player.Shoot();
    }

    private void UseItem(InputAction.CallbackContext context)
    {
        // Retrieve the item type using the action that triggered this method
        if (itemMappings.TryGetValue(context.action, out var itemType))
        {
            Player.Use(itemType);  // Use the item type found
        }
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;
        playerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}

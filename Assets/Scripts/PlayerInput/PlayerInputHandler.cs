using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } // Экспонируем направление движения для других компонентов

    private PlayerInputActions _playerInput;
    private PlayerInputActions PlayerInput { get
        {
            if(_playerInput == null)
                _playerInput = new PlayerInputActions();
            return _playerInput;
        } 
    }
    public Inventory inventory;
    Player Player;

    private Dictionary<InputAction, InventoryItemType> itemMappings = new();
    private void Awake()
    {
        inventory = ServiceLocator.Current.Get<Inventory>();
        Player = ServiceLocator .Current.Get<Player>();

        itemMappings = new Dictionary<InputAction, InventoryItemType>//здесь есть небольшая затычка с тем, что по первой кнопке
                                                                     //должен использоватся предмет, который лежит в первом слоте UI
        {
            { PlayerInput.Player.UseItem1, InventoryItemType.cottons },
            { PlayerInput.Player.UseItem2, InventoryItemType.milk },
            { PlayerInput.Player.UseItem3, InventoryItemType.rat }
        };
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        PlayerInput.Player.Enable();
        PlayerInput.Player.Move.performed += OnMove;
        PlayerInput.Player.Move.canceled += OnMove;
        foreach (var action in itemMappings.Keys)
        {
            action.performed += UseItem;
        }
        PlayerInput.Player.Shoot.performed += Shoot ;
        PlayerInput.Player.Shooting.performed += Shoot;
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
        PlayerInput.Player.Move.performed -= OnMove;
        PlayerInput.Player.Move.canceled -= OnMove;
        PlayerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}

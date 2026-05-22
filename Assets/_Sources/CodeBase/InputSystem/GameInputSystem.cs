using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputSystem : MonoBehaviour, IInputSystem
{
    private GameInput _gameInput;

    public event Action<Vector2> Clicked;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Player.Click.performed += OnClicked;
    }

    private void OnClicked(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Clicked?.Invoke(mouseScreenPosition);
    }

    private void OnEnable()
    {
        _gameInput.Enable();
    }

    private void OnDisable()
    {
        _gameInput.Disable();
    }

    private void OnDestroy()
    {
        if (_gameInput != null)
            _gameInput.Player.Click.performed -= OnClicked;
    }
}

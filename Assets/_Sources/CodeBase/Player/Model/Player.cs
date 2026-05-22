using UnityEngine;
[RequireComponent(typeof(GameInputSystem))]
[RequireComponent(typeof(PlayerInteractor))]
[RequireComponent(typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    private GameInputSystem _input;
    private PlayerInteractor _interactor;
    private PlayerAttack _attack;
    private Camera _mainCamera;

    private void Awake()
    {
        _input = GetComponent<GameInputSystem>();
        _interactor = GetComponent<PlayerInteractor>();
        _attack = GetComponent<PlayerAttack>();
        _mainCamera = Camera.main;

        _input.Clicked += OnClicked;
    }

    private void OnDestroy()
    {
        _input.Clicked -= OnClicked;
    }

    private void OnClicked(Vector2 position)
    {
        Debug.Log("Кликаю");
        if (TryGetTarget(position, out GameObject target))
        {
            if (target == null)
                return;
            else if (target.TryGetComponent<IDamageable>(out var damageable))
                _attack.Attack(damageable);
            else if (target.TryGetComponent<IInteractable>(out var interactable))
                _interactor.Interact(interactable);
        }
    }

    private bool TryGetTarget(Vector2 mousePosition, out GameObject target)
    {
        target = null;
        Vector2 worldPoint = _mainCamera.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider == null)
            return false;

        target = hit.collider.gameObject;
        return true;
    }
}
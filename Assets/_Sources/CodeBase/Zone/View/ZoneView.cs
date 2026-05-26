using TMPro;
using UnityEngine;

public class ZoneView : MonoBehaviour
{
    //Ёто должно быть UI HUD
    [SerializeField] private TextMeshProUGUI _levelView; // Ёто надо заменить на sprite

    private void Awake()
    {
        _levelView.SetText($"Level: {-999}");// ѕока дл€ теста.
    }

    private void OnEnable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged += SetLevel;
    }

    private void SetLevel(int value)
    {
        _levelView.SetText($"Level: {value}");
    }
}

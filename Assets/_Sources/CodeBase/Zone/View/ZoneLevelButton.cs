using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    private int _level;

    private void Awake()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public void Setup(int level, bool isUnlocked)
    {
        _level = level;
        _text.SetText(level.ToString());
        _button.interactable = isUnlocked;
    }

    public void SetSelected(bool isSelected)
    {
        //TODO: визуальное выделение — поменяй на своё
        _button.targetGraphic.color = isSelected ? Color.yellow : Color.white;
    }

    private void OnClicked()
    {
        GlobalContext.ZoneSystem.SelectLevel(_level);
    }
}
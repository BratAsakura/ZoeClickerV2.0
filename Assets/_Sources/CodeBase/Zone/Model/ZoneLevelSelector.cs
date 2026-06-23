using UnityEngine;
using UnityEngine.UI;

public class ZoneLevelSelector : MonoBehaviour
{
    [SerializeField] private int _visibleCount = 5;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private ZoneLevelButton[] _buttons;

    private int _pageOffset;
    private int _currentLevel;
    private int _maxUnlockedLevel;

    private void Awake()
    {
        _nextButton.onClick.AddListener(OnNextClicked);
        _prevButton.onClick.AddListener(OnPrevClicked);
    }

    private void OnEnable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged += OnZoneLevelChanged;
        GlobalContext.ZoneSystem.ZoneUnlocked += OnZoneUnlocked;
    }

    private void OnDisable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged -= OnZoneLevelChanged;
        GlobalContext.ZoneSystem.ZoneUnlocked -= OnZoneUnlocked;
    }

    private void OnDestroy()
    {
        _nextButton.onClick.RemoveListener(OnNextClicked);
        _prevButton.onClick.RemoveListener(OnPrevClicked);
    }

    private void Refresh()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int level = _pageOffset + i + 1;
            bool isUnlocked = level <= _maxUnlockedLevel;

            _buttons[i].Setup(level, isUnlocked);
            _buttons[i].SetSelected(level == _currentLevel);
        }

        _prevButton.interactable = _pageOffset > 0;
        _nextButton.interactable = _pageOffset + _visibleCount < _maxUnlockedLevel;
    }

    private void OnZoneLevelChanged(int level)
    {
        _currentLevel = level;
        _maxUnlockedLevel = GlobalContext.ZoneSystem.MaxUnlockedLevel;
        Refresh();
    }

    private void OnZoneUnlocked(int maxUnlockedLevel)
    {
        _maxUnlockedLevel = maxUnlockedLevel;
        _pageOffset = _maxUnlockedLevel - 5;
        Refresh();
    }

    private void OnNextClicked()
    {
        _pageOffset++;
        Refresh();
    }

    private void OnPrevClicked()
    {
        _pageOffset--;
        Refresh();
    }
}

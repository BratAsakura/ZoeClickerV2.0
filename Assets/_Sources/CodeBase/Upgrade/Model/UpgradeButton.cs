using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(UpgradeView))]
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeDataSO _data;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
        GetComponent<UpgradeView>().Init(_data);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        GlobalContext.UpgradeSystem.TryBuy(_data);
    }
}

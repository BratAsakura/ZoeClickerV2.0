using TMPro;
using UnityEngine;

public class WalleyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceView;

    private void Awake()
    {
        _balanceView.SetText("Money: 0");
    }

    private void OnEnable()
    {
        GlobalContext.WalletSystem.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        GlobalContext.WalletSystem.BalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(double amount)
    {
        _balanceView.SetText("Money: " + NumberFormatter.Format(amount));
    }
}
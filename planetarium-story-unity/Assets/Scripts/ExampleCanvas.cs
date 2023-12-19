using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExampleCanvas : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyText;
    
    [SerializeField]
    private Button getCurrencyButton;

    [SerializeField]
    private Button upgradeButton;
    
    [SerializeField]
    private TextMeshProUGUI upgradeText;
    
    private readonly ReactiveProperty<decimal> _currency = new(0);
    private readonly ReactiveProperty<decimal> _level = new(1);
    
    private decimal UpgradeCost => _level.Value * 10;
    private decimal CurrencyPerSecond => (_level.Value - 1) * 1;
    private decimal CurrencyPerClick => _level.Value * 10;

    private void Awake()
    {
        getCurrencyButton.onClick.AddListener(() => { _currency.Value += CurrencyPerClick; });

        upgradeButton.onClick.AddListener(() =>
        {
            if (_currency.Value >= UpgradeCost)
            {
                _currency.Value -= UpgradeCost;
                _level.Value += 1;
            }
            else
            {
                Debug.Log("Not enough currency!");
            }
        });

        _currency.Subscribe(OnCurrencyChanged);
        _level.Subscribe(OnLevelChanged);
    }
    
    private void Start()
    {
        Observable.Interval(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ => { _currency.Value += CurrencyPerSecond; })
            .AddTo(this);
    }

    private void OnCurrencyChanged(decimal currency)
    {
        currencyText.text = currency.ToString("N0");
    }
    
    private void OnLevelChanged(decimal level)
    {
        upgradeText.text = $"Upgrade \n{UpgradeCost:N0}";
    }
}

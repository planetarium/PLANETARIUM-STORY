using UniRx;

public enum GetCurrencyType
{
    Click,
    Second
}

public class Friend
{
    private GetCurrencyType GetCurrencyType => GetCurrencyType.Click;
    private decimal weight = 1;

    private readonly ReactiveProperty<decimal> _level = new(1);

    public decimal GetCurrency => _level.Value * 1;
}

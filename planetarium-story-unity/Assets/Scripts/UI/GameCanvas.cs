using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetariumStory.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI perTimeText;
        [SerializeField] private TextMeshProUGUI costPerTimeText;
        [SerializeField] private TextMeshProUGUI perClickText;
        [SerializeField] private TextMeshProUGUI costPerClickText;

        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private Button hiringButton;
        [SerializeField] private TeamBuffView teamBuffView;
        [SerializeField] private PlaceBuffView placeBuffView;
        [SerializeField] private TotalCountBuffView totalCountBuffView;
        [SerializeField] private HiringPopup hiringPopup;
        
        public void Init()
        {
            var logic = GameManager.Instance.Logic;
            var tableSheets = GameManager.Instance.TableSheets;
            
            logic.PerTime.Subscribe(perTime =>
            {
                perTimeText.text = $"{perTime}초 당 자동 획득";
            }).AddTo(gameObject);
            logic.GetCostPerTime.Subscribe(cost =>
            {
                costPerTimeText.text = cost.ToString("N0");
            }).AddTo(gameObject);
            logic.PerClick.Subscribe(perClick =>
            {
                perClickText.text = $"{perClick}회 클릭 당 획득";
            }).AddTo(gameObject);
            logic.GetCostClick.Subscribe(cost =>
            {
                costPerClickText.text = cost.ToString("N0");
            }).AddTo(gameObject);

            logic.Currency.Subscribe(currency =>
            {
                currencyText.text = currency.ToString("N0");
            }).AddTo(gameObject);
            logic.SpaceStep.Subscribe(step =>
            {
                placeBuffView.Set(step, tableSheets.ShopSpaceSheet);
            }).AddTo(gameObject);
            logic.OnChangeCharacters.Subscribe(characters =>
            {
                teamBuffView.Set(characters, tableSheets.TeamBuffSheet);
                totalCountBuffView.Set(characters, tableSheets.TotalCountBuffSheet);
            }).AddTo(gameObject);
            
            hiringButton.OnClickAsObservable().Subscribe(_ =>
            {
                hiringPopup.Show();
            }).AddTo(gameObject);
            
            hiringPopup.Init();
        }
    }
}
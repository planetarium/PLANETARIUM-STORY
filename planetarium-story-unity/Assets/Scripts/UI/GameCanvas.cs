using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetariumStory.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private Button hiringButton;
        [SerializeField] private TeamBuffView teamBuffView;
        [SerializeField] private PlaceBuffView placeBuffView;
        [SerializeField] private TotalCountBuffView totalCountBuffView;
        
        public void Set()
        {
            var logic = GameManager.Instance.Logic;
            var tableSheets = GameManager.Instance.TableSheets;

            logic.Currency.Subscribe(currecy =>
            {
                currencyText.text = $"{currecy}";
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
                Debug.Log("asdf");
            }).AddTo(gameObject);
        }
    }
}
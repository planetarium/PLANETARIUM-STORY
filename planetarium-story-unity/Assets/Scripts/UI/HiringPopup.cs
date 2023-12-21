using System.Linq;
using TableSheet;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetariumStory.UI
{
    public class HiringPopup : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private ProfileCell profileCellPrefab;
        [SerializeField] private Transform scrollViewContent;

        public void Init()
        {
            var logic = GameManager.Instance.Logic;
            var tableSheets = GameManager.Instance.TableSheets;

            logic.OnChangeCharacters.Subscribe(characters =>
            {
                Set(logic, tableSheets.ShopCharacterSheet);
            }).AddTo(gameObject);
            
            closeButton.OnClickAsObservable().Subscribe(_ =>
            {
                gameObject.SetActive(false);
                GameManager.Instance.canClick = true;
            }).AddTo(gameObject);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            GameManager.Instance.canClick = false;
            Set(GameManager.Instance.Logic, GameManager.Instance.TableSheets.ShopCharacterSheet);
        }
        
        private void Set(Logic logic, ShopCharacterSheet sheet)
        {
            var activatedCount = logic.Characters.Count(character => character.IsActivated);
            var cost = sheet[activatedCount + 1].Cost;

            var orderedCharacters = logic.Characters
                .OrderBy(character => character.IsActivated)
                .ThenBy(character => character.Row.Team)
                .ThenBy(character => character.Row.Id);
            
            foreach (Transform child in scrollViewContent)
            {
                Destroy(child.gameObject);
            }

            foreach (var character in orderedCharacters)
            {
                var profileCell = Instantiate(profileCellPrefab, scrollViewContent);
                profileCell.Set(character, cost, logic.Hire);
            }
        }
    }
}
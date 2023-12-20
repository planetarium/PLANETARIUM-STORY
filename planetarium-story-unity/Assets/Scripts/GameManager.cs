using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptableObjects;
using TableSheet;
using UnityEngine;

namespace PlanetariumStory
{
    using UniRx;
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private TableSheetContainer tableSheetContainer;

        public TableSheets TableSheets { get; private set; }
        public Logic Logic { get; private set; }

        private void Start()
        {
            Init();
            StartCoroutine(CoGetCostPerClick());
            StartCoroutine(CoGetCostPerTime());
        }

        private void Init()
        {
            #region TableSheets

            var csvAssets = tableSheetContainer.tableCsvAssets;
            var csv = new Dictionary<string, string>();
            foreach (var asset in csvAssets)
            {
                csv[asset.name] = asset.text;
            }

            TableSheets = new TableSheets(csv);
            TableSheets.GameConfigSheet.Update(TableSheets.GameConfigSheet.Values.ToList());

            #endregion

            #region TestData

            Logic = new Logic();
            
            var canvas = FindObjectOfType<UI.GameCanvas>();
            canvas.Init();
            
            #endregion
            
            Logic.OnChangeCharacters.Subscribe(characterList =>
            {
                var totalCountBuffSheet = TableSheets.TotalCountBuffSheet;
                var currentTotalCount = characterList.Count(character => character.IsActivated);
            
                var getCostClick = TableSheets.GameConfigSheet.GetCostClick;
                foreach (var row in totalCountBuffSheet.Values)
                {
                    if (currentTotalCount >= row.TotalCount)
                    {
                        getCostClick *= row.ClickCostBonus;
                    }
                }

                Logic.GetCostClick.Value = getCostClick;
                Debug.Log($"GetCostClick: {getCostClick}");

                var teamBuffSheet = TableSheets.TeamBuffSheet;
                var countByTeam = Logic.CountByTeam(characterList);
                var activatedCountByTeam = Logic.CountByTeam(characterList.Where(character => character.IsActivated));
                
                var getCostTime = TableSheets.GameConfigSheet.GetCostTime;
                foreach (var teamType in countByTeam.Keys)
                {
                    if (countByTeam[teamType] == activatedCountByTeam[teamType])
                    {
                        getCostTime *= teamBuffSheet[teamType].TimeCostBonus;
                    }
                }

                Logic.GetCostPerTime.Value = getCostTime;
                Debug.Log($"GetCostPerTime: {getCostTime}");
            }).AddTo(gameObject);
            
            Logic.SpaceStep.Subscribe(spaceStep =>
            {
                var defaultPerTime = TableSheets.GameConfigSheet.PerTime;
                var shopSpaceSheet = TableSheets.ShopSpaceSheet;
                var speed = shopSpaceSheet[spaceStep].BonusSpeed;
                Logic.PerTime.Value = defaultPerTime / speed;
            }).AddTo(gameObject);
            
            // Init values
            Logic.Currency.Value = 0;
            Logic.SpaceStep.Value = 1;

            var sb = new System.Text.StringBuilder();
            var characters = new List<Character>();
            foreach (var row in TableSheets.CharacterSheet.Values)
            {
                sb.AppendLine($"{row.Id}: {row.Name}, {row.Team}");
                var character = new Character(row);
                characters.Add(character);
            }

            Logic.Characters.AddRange(characters);
            Logic.OnChangeCharacters.OnNext(Logic.Characters);

            Debug.Log(sb);
            sb.Clear();
        }

        private IEnumerator CoGetCostPerTime()
        {
            yield return null;
            while (true)
            {
                yield return new WaitForSeconds(Logic.PerTime.Value);

                Logic.Currency.Value += Logic.GetCostPerTime.Value;                
            }
        }

        private IEnumerator CoGetCostPerClick()
        {
            yield return null;
            while (true)
            {
                yield return null;

                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    Logic.Currency.Value += Logic.GetCostClick.Value;
                }
            }
        }
    }
}
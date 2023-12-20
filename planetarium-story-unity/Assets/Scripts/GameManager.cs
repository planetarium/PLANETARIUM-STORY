using System.Collections;
using System.Collections.Generic;
using Script.ScriptableObjects;
using TableSheet;
using UnityEngine;

namespace PlanetariumStory
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private TableSheetContainer tableSheetContainer;

        public TableSheets TableSheets { get; private set; }
        public Logic Logic { get; private set; }

        private void Start()
        {
            Init();
            StartCoroutine(CoUpdate());
            

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

            #endregion

            #region TestData

            Logic = new Logic();
            
            var canvas = FindObjectOfType<UI.GameCanvas>();
            canvas.Set();
            
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


            #endregion
        }

        private IEnumerator CoUpdate()
        {
            yield return null;
            while (true)
            {
                yield return new WaitForSeconds(15);
                
                
            }
        }
    }
}
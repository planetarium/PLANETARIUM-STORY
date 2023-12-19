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
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            #region TableSheets

            yield return null;

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

            var sb = new System.Text.StringBuilder();
            var characters = new List<Character>();
            foreach (var row in TableSheets.CharacterSheet.Values)
            {
                sb.AppendLine($"{row.Id}: {row.Name}, {row.Team}");
                var character = new Character(row);
                characters.Add(character);
            }

            Logic.Characters.AddRange(characters);

            Debug.Log(sb);
            sb.Clear();


            #endregion
        }
    }
}
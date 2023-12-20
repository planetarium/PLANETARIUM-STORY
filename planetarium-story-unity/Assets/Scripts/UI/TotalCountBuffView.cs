using System.Collections.Generic;
using System.Linq;
using TableSheet;
using TMPro;
using UnityEngine;

namespace PlanetariumStory.UI
{
    public class TotalCountBuffView : BuffView
    {
        [SerializeField] private TextMeshProUGUI totalCountText;
        public void Set(List<Character> characters, TotalCountBuffSheet sheet)
        {
            var totalCount = characters.Count;
            var activatedCount = characters.Count(character => character.IsActivated);

            int i = 0;
            foreach (var row in sheet.Values)
            {
                if (i >= buffElements.Length)
                {
                    break;
                }

                var buffElement = buffElements[i++];
                buffElement.Set($"{row.TotalCount}명", $"x{row.ClickCostBonus}", activatedCount >= row.TotalCount);
            }
            
            totalCountText.text = $"{activatedCount}명/{totalCount}명";
        }
    }
}
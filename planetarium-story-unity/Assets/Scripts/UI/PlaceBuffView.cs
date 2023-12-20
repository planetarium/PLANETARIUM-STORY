using System.Collections.Generic;
using TableSheet;

namespace PlanetariumStory.UI
{
    public class PlaceBuffView : BuffView
    {
        private readonly Dictionary<int, string> _stepToText = new()
        {
            { 1, "기본 사무실" },
            { 2, "확장 사무실" },
            { 3, "라운지" },
        };

        public void Set(int step, ShopSpaceSheet sheet)
        {
            int i = 0;
            foreach (var row in sheet.Values)
            {
                if (i >= buffElements.Length)
                {
                    break;
                }

                var buffElement = buffElements[i++];
                buffElement.Set($"{row.Step} : {_stepToText[row.Step]}", $"SPD {row.BonusSpeed}", step >= row.Step);
            }
        }
    }
}
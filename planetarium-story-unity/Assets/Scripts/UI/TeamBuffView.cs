using System.Collections.Generic;
using System.Linq;
using TableSheet;

namespace PlanetariumStory.UI
{
    public class TeamBuffView : BuffView
    {
        public void Set(List<Character> characters, TeamBuffSheet sheet)
        {
            var countByTeam = Logic.CountByTeam(characters);
            var activatedCountByTeam = Logic.CountByTeam(characters.Where(character => character.IsActivated));

            int i = 0;
            foreach (var teamType in countByTeam.Keys)
            {
                if (i >= buffElements.Length)
                {
                    break;
                }

                var count = countByTeam[teamType];
                var activatedCount = activatedCountByTeam[teamType];
                var effect = sheet[teamType].TimeCostBonus;
                
                var isActive = count == activatedCount;
                var buffElement = buffElements[i++];
                buffElement.Set($"{teamType.ToString()} ({activatedCount}/{count})", $"x{effect}", isActive);
            }
        }
    }
}
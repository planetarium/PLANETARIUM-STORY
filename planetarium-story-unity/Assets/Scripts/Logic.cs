using System;
using System.Collections.Generic;
using System.Linq;
using TableSheet;
using UniRx;

namespace PlanetariumStory
{
    public class Logic
    {
        public readonly ReactiveProperty<decimal> Currency = new(0);
        public readonly ReactiveProperty<int> SpaceStep = new(1);

        public readonly List<Character> Characters = new();
        public int TotalCount => Characters.Count;

        public Dictionary<TeamType, int> CountByTeam
        {
            get
            {
                return Enum.GetValues(typeof(TeamType)).Cast<TeamType>().ToArray().ToDictionary(
                    teamType => teamType,
                    teamType => Characters.Count(character => character.Row.Team == teamType));
            }
        }

        public int TotalActivatedCount => Characters.Count(character => character.IsActivated);

        public Dictionary<TeamType, int> ActivatedCountByTeam
        {
            get
            {
                var characters = Characters.Where(character => character.IsActivated).ToArray();
                return Enum.GetValues(typeof(TeamType)).Cast<TeamType>().ToArray().ToDictionary(
                    teamType => teamType,
                    teamType => characters.Count(character => character.Row.Team == teamType));
            }
        }
    }
}
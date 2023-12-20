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
        public readonly Subject<List<Character>> OnChangeCharacters = new();

        public static Dictionary<TeamType, int> CountByTeam(IEnumerable<Character> characters)
        {
            return Enum.GetValues(typeof(TeamType)).Cast<TeamType>().ToDictionary(
                teamType => teamType,
                teamType => characters.Count(character => character.Row.Team == teamType));
        }

        public readonly ReactiveProperty<int> PerClick = new(1);
        public readonly ReactiveProperty<long> GetCostClick = new(1);
        public readonly ReactiveProperty<float> PerTime = new(1);
        public readonly ReactiveProperty<long> GetCostPerTime = new(1);
    }
}
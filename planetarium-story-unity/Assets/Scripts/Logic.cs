using System;
using System.Collections.Generic;
using System.Linq;
using TableSheet;
using UniRx;
using UnityEngine;

namespace PlanetariumStory
{
    public class Logic
    {
        public readonly ReactiveProperty<decimal> Currency = new(0);
        public readonly ReactiveProperty<int> SpaceStep = new(1);

        public readonly List<Character> Characters = new();
        public readonly Subject<List<Character>> OnChangeCharacters = new();
        public readonly Subject<Character> OnHire = new();

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

        public void Hire(int id)
        {
            // try hire
            var c = Characters.FirstOrDefault(c => c.Row.Id == id);
            if (c == null)
            {
                return;
            }

            var activatedCount = Characters.Count(character => character.IsActivated);
            var cost = GameManager.Instance.TableSheets.ShopCharacterSheet[activatedCount + 1].Cost;
            if (Currency.Value < cost)
            {
                return;
            }
                        
            Currency.Value -= cost;
            c.Activate();
            OnChangeCharacters.OnNext(Characters);
            OnHire.OnNext(c);
            Debug.Log($"Hire {c.Row.Name}");
        }
        
        public void UpgradeSpace()
        {
            var cost = GameManager.Instance.TableSheets.ShopSpaceSheet[SpaceStep.Value].Cost;
            if (Currency.Value < cost)
            {
                return;
            }
            
            Currency.Value -= cost;
            SpaceStep.Value++;
            Debug.Log($"Upgrade Space {SpaceStep.Value}");
        }
    }
}
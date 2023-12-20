using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetariumStory.UI
{
    public class ProfileCell : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI teamText;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private GameObject activated;
        
        public void Set(Character character, long cost, Action<int> onClick)
        {
            image.sprite = GetSprite(character.Row.Id);
            nameText.text = character.Row.Name;
            teamText.text = character.Row.Team.ToString();
            costText.text = GetCostString(cost);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke(character.Row.Id));
            activated.SetActive(character.IsActivated);
        }

        private static Sprite GetSprite(int id)
        {
            return Resources.Load<Sprite>($"Sprites/faces/{id}");
        }

        private static string GetCostString(long cost)
        {
            // 값에 따라 K, M, B으로 줄여 표기
            if (cost < 1e3)
            {
                return $"{cost}";
            }

            if (cost < 1e6)
            {
                return $"{cost / 1e3}K";
            }

            if (cost < 1e9)
            {
                return $"{cost / 1e6}M";
            }

            return $"{cost / 1e9}B";
        }
    }
}

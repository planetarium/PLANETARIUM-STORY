using TMPro;
using UnityEngine;

namespace PlanetariumStory.UI
{
    public class BuffElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI conditionText;
        [SerializeField] private Color activeConditionColor;
        [SerializeField] private Color inactiveConditionColor;

        [SerializeField] private TextMeshProUGUI effectText;
        [SerializeField] private Color activeEffectColor;
        [SerializeField] private Color inactiveEffectColor;

        public void Set(string condition, string effect, bool isActive)
        {
            conditionText.text = condition;
            conditionText.color = isActive ? activeConditionColor : inactiveConditionColor;

            effectText.text = effect;
            effectText.color = isActive ? activeEffectColor : inactiveEffectColor;
        }
    }
}
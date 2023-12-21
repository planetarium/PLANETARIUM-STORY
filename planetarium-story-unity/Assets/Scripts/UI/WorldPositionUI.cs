using UnityEngine;

namespace PlanetariumStory.UI
{
    public class WorldPositionUI : Singleton<WorldPositionUI>
    {
        [SerializeField] private DialogTooltip dialogTooltipPrefab;

        public DialogTooltip InstantiateDialogTooltip()
        {
            var dialogTooltip = Instantiate(dialogTooltipPrefab, transform);
            dialogTooltip.Hide();
            return dialogTooltip;
        }
    }
}
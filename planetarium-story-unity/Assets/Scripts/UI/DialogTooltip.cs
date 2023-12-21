using System.Collections;
using TMPro;
using UnityEngine;

namespace PlanetariumStory.UI
{
    public class DialogTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float yOffset = 100f;
        
        private Unit _unit;
        private RectTransform _canvasRectTransform;

        public void Show(string text, Unit unit)
        {
            gameObject.SetActive(true);
            
            _unit = unit;
            StartCoroutine(CoUpdate());
            _canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
            
            dialogText.text = text;
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }

        private IEnumerator CoUpdate()
        {
            while (true)
            {
                yield return null;
                var canvasRect = _canvasRectTransform;
                Vector2 viewportPosition = Camera.main.WorldToViewportPoint(_unit.transform.position);
                var worldObjectScreenPosition = new Vector2(
                    viewportPosition.x * canvasRect.sizeDelta.x - canvasRect.sizeDelta.x * 0.5f,
                    viewportPosition.y * canvasRect.sizeDelta.y - canvasRect.sizeDelta.y * 0.5f + yOffset);
                rectTransform.anchoredPosition = worldObjectScreenPosition;
            }
        }
    }
}
using System;
using System.Linq;
using PlanetariumStory.UI;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlanetariumStory
{
    [Serializable]
    public class ObjectsOnPlaceStep
    {
        public int PlaceStep;
        public GameObject[] Objects;
    }
    
    public class Map : MonoBehaviour
    {
        [SerializeField] private Transform unitContainer;
        [SerializeField] private Unit[] unitPrefabs;
        
        [SerializeField] private ObjectsOnPlaceStep[] objectsOnPlaceSteps;
        
        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;
        
        public void Init(Logic logic)
        {
            foreach (var character in logic.Characters.Where(character => character.IsActivated))
            {
                InstantiateUnit(character);
            }
            
            logic.OnHire.Subscribe(InstantiateUnit).AddTo(gameObject);
            logic.SpaceStep.Subscribe(step =>
            {
                foreach (var objectsOnPlaceStep in objectsOnPlaceSteps)
                {
                    foreach (var obj in objectsOnPlaceStep.Objects)
                    {
                        obj.SetActive(step >= objectsOnPlaceStep.PlaceStep);
                    }
                }
            }).AddTo(gameObject);
        }

        private void InstantiateUnit(Character character)
        {
            var row = GameManager.Instance.TableSheets.CharacterDialogueSheet[character.Row.Id];
            var unitPrefab = unitPrefabs[Random.Range(0, unitPrefabs.Length)];
            var unit = Instantiate(unitPrefab, unitContainer);
            unit.Set(row, WorldPositionUI.Instance.InstantiateDialogTooltip());
            unit.transform.position = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y), 0);
        }
    }
}
using System.Collections;
using DG.Tweening;
using PlanetariumStory.UI;
using TableSheet;
using UnityEngine;

namespace PlanetariumStory
{
    public class Unit : MonoBehaviour
    {
        // animation -> 랜덤 초에 한 번 씩 실행
        // character -> 대사 가져와서 출력
        // 돌아다니기
        // 초기 세팅
        
        [SerializeField] private SpriteRenderer bodySpriteRenderer;
        [SerializeField] private SpriteRenderer faceSpriteRenderer;
        [SerializeField] private Animator animator;
        
        private CharacterDialogueSheet.Row _row;
        private DialogTooltip _dialogTooltip;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Hooray = Animator.StringToHash("Hooray");
        private static readonly int Thinking = Animator.StringToHash("Thinking");
        private static readonly int Dance = Animator.StringToHash("Dance");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public void Set(CharacterDialogueSheet.Row row, DialogTooltip dialogTooltip)
        {
            _row = row;
            _dialogTooltip = dialogTooltip;
            faceSpriteRenderer.sprite = GetSprite(_row.Id);
            
            StartCoroutine(CoUpdate());
        }
        
        private static Sprite GetSprite(int id)
        {
            return Resources.Load<Sprite>($"Sprites/faces/{id}");
        }

        private IEnumerator CoUpdate()
        {
            var dialogue = _row.Dialogues;
            var animatorTrigger = new[]
            {
                Walk, Hooray, Thinking, Dance, Idle
            };
            var directions = new[]
            {
                Vector3.up, Vector3.down, Vector3.left, Vector3.right
            };

            while (true)
            {
                animator.SetTrigger(Idle);
                _dialogTooltip.Hide();
                
                var position = transform.position;
                if (position.x is < -18f or > 24f || position.y is < -14 or > 16)
                {
                    transform.position = Vector3.zero;
                }
                
                yield return new WaitForSeconds(Random.Range(1f, 5f));

                if (!RandomUtility.Probability(40f))
                {
                    _dialogTooltip.Show($"[{_row.Name}] : {dialogue[Random.Range(0, dialogue.Length)]}", this);
                }

                var trigger = animatorTrigger[Random.Range(0, animatorTrigger.Length)];
                animator.SetTrigger(trigger);
                if (trigger == Walk)
                {
                    var direction = directions[Random.Range(0, directions.Length)];
                    transform.DOMove(transform.position + direction * 5, 3f);
                    bodySpriteRenderer.flipX = direction.x > 0;
                }

                yield return new WaitForSeconds(3f);
            }
        }
    }
}
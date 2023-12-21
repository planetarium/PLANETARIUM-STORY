using System.Collections;
using Cinemachine;
using UniRx;
using UnityEngine;

namespace PlanetariumStory
{
    public class InputController : MonoBehaviour
    {
        public Subject<Unit> OnClickScreen { get; } = new();
        public Subject<int> OnClickUnlockSpace { get; } = new();
        public Subject<int> OnClickCurrencyItem { get; } = new();

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform cameraFollowTransform;
        [SerializeField] private float dragSpeed = 200f;
        [SerializeField] private float scrollSpeed = 200f;

        public void Init()
        {
            StartCoroutine(CoUpdate());
        }

        private IEnumerator CoUpdate()
        {
            while (true)
            {
                yield return null;

                if (!GameManager.Instance.canClick)
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    var xDragMove = Input.GetAxis("Mouse X") * Time.deltaTime * dragSpeed;
                    var yDragMove = Input.GetAxis("Mouse Y") * Time.deltaTime * dragSpeed;
                    cameraFollowTransform.Translate(-xDragMove, -yDragMove, 0f);
                }
                else
                {
                    var scrollWheel = Input.GetAxis("Mouse ScrollWheel");

                    virtualCamera.m_Lens.OrthographicSize -= scrollWheel * Time.deltaTime * scrollSpeed;
                    virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, 2f, 8f);
                }
            }
        }
    }
}
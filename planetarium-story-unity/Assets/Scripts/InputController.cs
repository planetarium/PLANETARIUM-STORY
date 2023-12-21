using System.Collections;
using Cinemachine;
using UnityEngine;

namespace PlanetariumStory
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform cameraFollowTransform;
        [SerializeField] private float dragSpeed = 200f;
        [SerializeField] private float scrollSpeed = 200f;
        [SerializeField] private float minOrthographicSize = 2f;
        [SerializeField] private float maxOrthographicSize = 12f;

        public void Init()
        {
            StartCoroutine(CoUpdate());
        }

        private IEnumerator CoUpdate()
        {
            yield return new WaitForSeconds(3f);

            while (true)
            {
                yield return null;

                if (!GameManager.Instance.canClick)
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    var modifier = Time.deltaTime * dragSpeed * virtualCamera.m_Lens.OrthographicSize;
                    var xDragMove = Input.GetAxis("Mouse X") * modifier;
                    var yDragMove = Input.GetAxis("Mouse Y") * modifier;
                    cameraFollowTransform.Translate(-xDragMove, -yDragMove, 0f);
                    
                    var position = cameraFollowTransform.transform.position;
                    cameraFollowTransform.transform.position = new Vector3(
                        Mathf.Clamp(position.x, -18f, 24f),
                        Mathf.Clamp(position.y, -14f, 16f),
                        position.z);
                }
                else
                {
                    var scrollWheel = Input.GetAxis("Mouse ScrollWheel");

                    virtualCamera.m_Lens.OrthographicSize -= scrollWheel * Time.deltaTime * scrollSpeed;
                    virtualCamera.m_Lens.OrthographicSize =
                        Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, minOrthographicSize, maxOrthographicSize);
                }
            }
        }
    }
}
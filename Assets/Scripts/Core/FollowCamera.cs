using UnityEngine;

namespace Toon.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] GameObject player;

        [SerializeField] float updateTimeOffset = 5f;
        float startTimeOffset = 200f;
        [SerializeField] Vector2 posOffset;

        void Awake()
        {
            CameraPositionUpdate(startTimeOffset);
        }

        void LateUpdate()
        {
            if (player == null) return;
            CameraPositionUpdate(updateTimeOffset);
        }

        private void CameraPositionUpdate(float timeOffset)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x,
                                         0,
                                         transform.position.z);
            endPos.x += posOffset.x;
            endPos.y += posOffset.y;
            endPos.z = transform.position.z;

            transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
        }
    }
}

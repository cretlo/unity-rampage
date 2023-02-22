using UnityEngine;

namespace WarriorAnims
{
    public class CameraController:MonoBehaviour
    {
        public GameObject cameraTarget;
        public float cameraTargetOffsetY = 1.0f;
        private Vector3 cameraTargetOffset;
        public float rotateSpeed = 1.0f;
        private float rotate;
        public float height = 2.75f;
        public float distance = 1.25f;
        public float zoomAmount = 0.2f;
        public float smoothing = 2.0f;
        private Vector3 offset;
        private bool following = true;
        private Vector3 lastPosition;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            // Follow cam.

            // Rotate cam.
            if (Input.GetKey(KeyCode.Q)) { rotate = -1; }
            else if (Input.GetKey(KeyCode.E)) { rotate = 1; }
            else { rotate = 0; }

            // Mouse zoom.
            if (Input.mouseScrollDelta.y == 1) { distance += zoomAmount; height += zoomAmount; }
            else if (Input.mouseScrollDelta.y == -1) { distance -= zoomAmount; height -= zoomAmount; }

            // Set cameraTargetOffset as cameraTarget + cameraTargetOffsetY.
            
        }
        
        private void LateUpdate()
        {  }
    }
}
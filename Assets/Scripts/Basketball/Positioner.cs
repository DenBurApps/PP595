using UnityEngine;

namespace Basketball
{
    public class Positioner : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _screenPositionPercentage;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            PositionBasket();
        }

        private void PositionBasket()
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            float targetX = screenWidth * _screenPositionPercentage.x;
            float targetY = screenHeight * _screenPositionPercentage.y;

            Vector2 screenPos = new Vector2(targetX, targetY);

            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPos);

            _transform.position = worldPos;
        }
    }
}

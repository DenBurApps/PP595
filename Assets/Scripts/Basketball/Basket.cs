using System;
using UnityEngine;

namespace Basketball
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Basket : MonoBehaviour
    {
        private BoxCollider2D _collider;

        public event Action BallDetected;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider != null && collider.TryGetComponent(out BasketBall ball))
            {
                BallDetected?.Invoke();
            }
        }
    }
    
}

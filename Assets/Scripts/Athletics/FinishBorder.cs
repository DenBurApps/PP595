using System;
using UnityEngine;

namespace AthleticsRace
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FinishBorder : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;

        public event Action<IInteractable> Finished;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.isTrigger = true;
        }

        public void EnableTrigger()
        {
            _boxCollider.isTrigger = true;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                Finished?.Invoke(interactable);
                _boxCollider.isTrigger = false;
            }
        }
    }
    
}


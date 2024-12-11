using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Gate : MonoBehaviour
{
    private BoxCollider2D _collider;

    public event Action BallCollided;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void SetGateAsTrigger()
    {
        _collider.isTrigger = true;
    }

    public void DiactivateGateTrigger()
    {
        _collider.isTrigger = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.TryGetComponent(out Ball ball))
        {
            BallCollided?.Invoke();
        }
    }
}

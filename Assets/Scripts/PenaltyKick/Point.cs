using UnityEngine;

public class Point : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _defaultPosition;

    private void Awake()
    {
        _transform = transform;
        _defaultPosition = _transform.position;
    }

    public void SetPosition(Vector2 position)
    {
        _transform.position = position;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        _transform.position = _defaultPosition;
        gameObject.SetActive(false);
    }
}

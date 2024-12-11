using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _arrow;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _throwForceMultiplier;
    [SerializeField] private float _dragValue;

    private Transform _transform;
    private Vector2 _defaultPosition;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private Quaternion _defaultRotation;
    private Rigidbody2D _rigidbody2D;
    private IEnumerator _currentCoroutine;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Transform Transform => _transform;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _maxRotation = Quaternion.Euler(0, 0, 90);
        _minRotation = Quaternion.Euler(0, 0, -90);

        _transform = transform;
        _defaultPosition = _transform.position;
        _defaultRotation = _transform.rotation;
    }

    public void FreezeMovement()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        
        _arrow.gameObject.SetActive(false);
    }

    public Vector2 GetDirectionVector()
    {
        return _transform.TransformDirection(Vector2.up);
    }

    public void ReturnToDefaultPosition()
    {
        _transform.position = _defaultPosition;
        _transform.rotation = _defaultRotation;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        _arrow.gameObject.SetActive(true);
    }

    public void SetThrowForce(float force)
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        Vector2 throwForce = _transform.TransformDirection(Vector2.up) * force * _throwForceMultiplier;
        
        _rigidbody2D.AddForce(throwForce);
    }

    public void StartMoving()
    {
        if (_transform.rotation != _maxRotation)
        {
            StartTurningToMaxRotation();
        }
        else
        {
            StartTurningToMinRotation();
        }
    }

    private void StartTurningToMaxRotation()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = TurnToMaxRotation();
        StartCoroutine(_currentCoroutine);
    }

    private void StartTurningToMinRotation()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = TurnToMinRotation();
        StartCoroutine(_currentCoroutine);
    }

    private IEnumerator TurnToMaxRotation()
    {
        while (Quaternion.Angle(_transform.rotation, _maxRotation) > 0.01f)
        {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _maxRotation, Time.deltaTime * _turningSpeed);

            yield return null;
        }
        
        _transform.rotation = _maxRotation;
        StartTurningToMinRotation();
    }

    private IEnumerator TurnToMinRotation()
    {
        while (Quaternion.Angle(_transform.rotation, _minRotation) > 0.01f)
        {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _minRotation, Time.deltaTime * _turningSpeed);

            yield return null;
        }
        
        _transform.rotation = _minRotation;
        StartTurningToMaxRotation();
    }
}
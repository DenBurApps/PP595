using System;
using System.Collections;
using UnityEngine;

namespace Basketball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasketBall : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier;

        private readonly float _minimumSwipeThreshold = 30f;
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        private bool _swipeDetected = false;
        private Vector2 _defaultPosition;
        private IEnumerator _touchCoroutine;

        public event Action BallThrown; 
        
        private void Awake()
        {
            _transform = transform;
            _defaultPosition = _transform.position;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody2D.isKinematic = true;
            EnableTouch();
        }

        public void EnableTouch()
        {
            DisableTouch();

            _touchCoroutine = DetectTouchInput();
            StartCoroutine(_touchCoroutine);
        }

        public void DisableTouch()
        {
            if (_touchCoroutine != null)
            {
                StopCoroutine(_touchCoroutine);
                _touchCoroutine = null;
            }
        }

        private IEnumerator DetectTouchInput()
        {
            while (enabled)
            {
                if (Input.touchCount > 0 && !_swipeDetected)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        _startTouchPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        _endTouchPosition = touch.position;
                        
                        if (Vector2.Distance(_startTouchPosition, _endTouchPosition) > _minimumSwipeThreshold)
                        {
                            _swipeDetected = true;
                            ThrowBall();
                        }
                    }
                }

                yield return null;
            }
        }

        private void ThrowBall()
        {
            if (_swipeDetected)
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.None;
                Vector2 swipeDirection = _endTouchPosition - _startTouchPosition;

                Vector2 throwForce = swipeDirection.normalized * _forceMultiplier;

                _rigidbody2D.isKinematic = false;
                _rigidbody2D.drag = 0.2f;
                _rigidbody2D.AddForce(throwForce, ForceMode2D.Impulse);
                _swipeDetected = false;
                BallThrown?.Invoke();
            }
        }

        public void ResetBallPosition()
        {
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.drag = 0;
            _transform.position = _defaultPosition;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
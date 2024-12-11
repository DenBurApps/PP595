using System.Collections;
using UnityEngine;

namespace AthleticsRace
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour,IInteractable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private float _slowdownSpeed;
        [SerializeField] private float _minSlowdownInterval = 3f;
        [SerializeField] private float _maxSlowdownInterval = 8f;
        [SerializeField] private float _slowdownDuration = 2f;
        
        private Vector2 _defaultPosition;
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private bool _isSlowingDown;
        private IEnumerator _randomSlowdownCoroutine;
        private IEnumerator _touchDetectionCoroutine;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _defaultPosition = _transform.position;
            _movingSpeed = _defaultSpeed;
            _rigidbody.drag = 0;
        }
        
        public void DisableSprite()
        {
            _spriteRenderer.enabled = false;
        }

        public void EnableSprite()
        {
            _spriteRenderer.enabled = true;
        }

        public void EnableInput()
        {
            DisableInput();
            
            _touchDetectionCoroutine = DetectTouchInput();
            StartCoroutine(_touchDetectionCoroutine);
            
            //StartRandomSlowdown();
        }

        public void DisableInput()
        {
            if (_touchDetectionCoroutine != null)
            {
                StopCoroutine(_touchDetectionCoroutine);
                _touchDetectionCoroutine = null;
            }
            
           // StopRandomSlowdown();
            _rigidbody.velocity = Vector2.zero;
        }

        public void SetCharacterSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void ReturnToDefault()
        {
            _rigidbody.velocity = Vector2.zero;
            _transform.position = _defaultPosition;
        }

        private IEnumerator DetectTouchInput()
        {
            while (enabled)
            {
                if (Input.touchCount > 0)
                {
                    _movingSpeed += _speedMultiplier * Time.deltaTime;
                }
                else
                {
                    _movingSpeed -= _speedMultiplier * Time.deltaTime;
                }

                _movingSpeed = Mathf.Clamp(_movingSpeed, _defaultSpeed, _maxSpeed);

                _rigidbody.velocity = new Vector2(_movingSpeed, _rigidbody.velocity.y);

                yield return null;
            }
        }
        
        private void StartRandomSlowdown()
        {
            StopRandomSlowdown();
            _randomSlowdownCoroutine = RandomSlowdown();
            StartCoroutine(_randomSlowdownCoroutine);
        }

        private void StopRandomSlowdown()
        {
            if (_randomSlowdownCoroutine != null)
            {
                StopCoroutine(_randomSlowdownCoroutine);
                _randomSlowdownCoroutine = null;
            }
        }
        
        private IEnumerator RandomSlowdown()
        {
            while (enabled)
            {
                float waitTime = Random.Range(_minSlowdownInterval, _maxSlowdownInterval);
                yield return new WaitForSeconds(waitTime);
                
                float originalSpeed = _movingSpeed;
                _movingSpeed = _slowdownSpeed;

                yield return new WaitForSeconds(_slowdownDuration);

                _movingSpeed = Mathf.Clamp(originalSpeed, _defaultSpeed, _maxSpeed);
            }
        }

    }
    
}


using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AthleticsRace
{
    public class CountdownScreen : MonoBehaviour
    {
        private const string StartText = "Tap when you are ready to start the sprint";
        private const string RunText = "RUN!";
        private const int CountdownValue = 3;
        private const int IntervalBeforeStart = 2;
        private const int CountdownInterval = 1;
        
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _text;

        private IEnumerator _countdownCoroutine;
        
        public event Action GameStarted;

        private void OnEnable()
        {
            _text.text = StartText;
            _backgroundImage.enabled = true;
            _countdownCoroutine = null;
        }

        private void OnDisable()
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
                _countdownCoroutine = null;
            }
        }

        private void Update()
        {
            if (Input.touchCount > 0 && _countdownCoroutine == null)
            {
                _countdownCoroutine = StartCountdown();
                StartCoroutine(_countdownCoroutine);
            }
        }

        public void EnableScreen()
        {
            gameObject.SetActive(true);
        }

        public void DisableScreen()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator StartCountdown()
        {
            WaitForSeconds interval = new WaitForSeconds(CountdownInterval);
            WaitForSeconds intervalBeforeStart = new WaitForSeconds(IntervalBeforeStart);

            _backgroundImage.enabled = false;
            int countdown = CountdownValue;

            while (countdown > 0)
            {
                _text.text = countdown.ToString();
                countdown--;

                yield return interval;
            }

            _text.text = RunText;
            yield return intervalBeforeStart;
            GameStarted?.Invoke();
        }
    }
}
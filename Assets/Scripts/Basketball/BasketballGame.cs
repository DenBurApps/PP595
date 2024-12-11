using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basketball
{
    public class BasketballGame : MonoBehaviour
    {
        private const int WaitBeforeRestartTime = 2;

        [SerializeField] private BasketballGameView _view;
        [SerializeField] private BasketBall _ball;
        [SerializeField] private Basket _basket;
        [SerializeField] private YouMissedScreen _youMissedScreen;

        private int _score;
        private IEnumerator _waitAndLostCoroutine;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        private void OnEnable()
        {
            _view.ExitClicked += ExitGame;
            _youMissedScreen.TryAgainClicked += StartNewGame;

            _basket.BallDetected += ProcessWin;

            _ball.BallThrown += DetectGameLost;
        }

        private void OnDisable()
        {
            _view.ExitClicked -= ExitGame;
            _youMissedScreen.TryAgainClicked -= StartNewGame;

            _basket.BallDetected -= ProcessWin;

            _ball.BallThrown -= DetectGameLost;
        }

        private void Start()
        {
            _score = 0;
            StartNewGame();
        }

        private void StartNewGame()
        {
            _view.SetScoreText(_score);

            _ball.DisableTouch();
            _ball.ResetBallPosition();
            _ball.EnableTouch();
            _youMissedScreen.Disable();
            _view.SetDefaultColor();
        }

        private IEnumerator WaitAndProcessGameLost()
        {
            yield return new WaitForSeconds(WaitBeforeRestartTime);
            ProcessGameLost();
        }

        private void DetectGameLost()
        {
            _waitAndLostCoroutine = WaitAndProcessGameLost();
            StartCoroutine(_waitAndLostCoroutine);
        }

        private void ProcessGameLost()
        {
            _score = 0;
            _ball.ResetBallPosition();
            _youMissedScreen.Enable();
            _view.SetTranparent();
        }

        private void ProcessWin()
        {
            if (_waitAndLostCoroutine != null)
            {
                StopCoroutine(_waitAndLostCoroutine);
                _waitAndLostCoroutine = null;
            }

            _score++;

            PrefsScoreSaver.SaveScore("BasketballScore", _score);
            StartNewGame();
        }

        private void ExitGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
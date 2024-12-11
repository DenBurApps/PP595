using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basketball
{
    public class BasketballGameView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _fieldImage;
        [SerializeField] private Image _background;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _transparentColor;

        public event Action ExitClicked;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        public void SetScoreText(int score)
        {
            _score.text = score.ToString() + " scores";
        }
        
        public void SetTranparent()
        {
            _background.color = _transparentColor;
            _fieldImage.color = _transparentColor;
        }

        public void SetDefaultColor()
        {
            _background.color = _defaultColor;
            _fieldImage.color = _defaultColor;
        }

        private void OnExitButtonClicked() => ExitClicked?.Invoke();

    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Articles
{
    [RequireComponent(typeof(ScreenVisabilityHandler))]
    public class OpenArticleScreen : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;

        private ScreenVisabilityHandler _screenVisabilityHandler;

        public event Action BackClicked;

        private void Awake()
        {
            _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void Start()
        {
            _screenVisabilityHandler.DisableScreen();
        }

        public void OpenScreen(ArticleData data)
        {
            _image.sprite = data.Image;
            _title.text = data.Title;
            _content.text = data.Content;
            _screenVisabilityHandler.EnableScreen();
        }

        private void OnBackButtonClicked()
        {
            BackClicked?.Invoke();
            _screenVisabilityHandler.DisableScreen();
        }
    }
}


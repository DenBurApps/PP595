using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Articles
{
    [RequireComponent(typeof(ScreenVisabilityHandler))]
    public class ArticlesMainScreen : MonoBehaviour
    {
        [SerializeField] private ArticlePlane[] _articlePlanes;
        [SerializeField] private Button _gamesButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private MainScreen _mainScreen;
        [SerializeField] private OpenArticleScreen _openArticleScreen;
        [SerializeField] private TMP_InputField _search;
        [SerializeField] private GameObject _emptyPlane;
        [SerializeField] private Button _clearSearchButton;
        [SerializeField] private Image _searchImage;
        [SerializeField] private SettingsScreen _settingsScreen;

        private ScreenVisabilityHandler _screenVisabilityHandler;
        
        public event Action GamesClicked;
        public event Action SettingsClicked;

        private void Awake()
        {
            _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
        }

        private void OnEnable()
        {
            _mainScreen.ArticlesOpen += _screenVisabilityHandler.EnableScreen;
            _openArticleScreen.BackClicked += _screenVisabilityHandler.EnableScreen;
            _settingsScreen.ArticlesButtonClicked += _screenVisabilityHandler.EnableScreen;
            
            _gamesButton.onClick.AddListener(OnGamesClicked);
            _settingsButton.onClick.AddListener(OnSettingsClicked);
            _clearSearchButton.onClick.AddListener(ClearSearch);
            
            _search.onValueChanged.AddListener(SearchInputed);

            foreach (var plane in _articlePlanes)
            {
                plane.OpenButtonClicked += OpenArticle;
            }
            
            ToggleEmptyPlane(false);
            _clearSearchButton.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _mainScreen.ArticlesOpen -= _screenVisabilityHandler.EnableScreen;
            _openArticleScreen.BackClicked -= _screenVisabilityHandler.EnableScreen;
            _clearSearchButton.onClick.RemoveListener(ClearSearch);
            _settingsScreen.ArticlesButtonClicked -= _screenVisabilityHandler.EnableScreen;
            
            _gamesButton.onClick.RemoveListener(OnGamesClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsClicked);
            _search.onValueChanged.RemoveListener(SearchInputed);

            foreach (var plane in _articlePlanes)
            {
                plane.OpenButtonClicked -= OpenArticle;
            }
        }

        private void Start()
        {
            _screenVisabilityHandler.DisableScreen();
        }

        private void ClearSearch()
        {
            _search.text = string.Empty;
            VerifyActivePlanes();
        }

        private void OpenArticle(ArticleData data)
        {
            _openArticleScreen.OpenScreen(data);
        }

        private void OnGamesClicked()
        {
            GamesClicked?.Invoke();
            _screenVisabilityHandler.DisableScreen();
        }

        private void OnSettingsClicked()
        {
            SettingsClicked?.Invoke();
            _screenVisabilityHandler.DisableScreen();
        }
        
        private void SearchInputed(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                EnableAllSubscriptionWindows();
                _clearSearchButton.gameObject.SetActive(false);
                _searchImage.enabled = true;
                return;
            }

            _clearSearchButton.gameObject.SetActive(true);
            _searchImage.enabled = false;
            string adaptedSearch = text.ToLower();
            bool anyActive = false;

            foreach (var plane in _articlePlanes)
            {
                if (plane.Data.Title.ToLower().StartsWith(adaptedSearch.Substring(0, Mathf.Min(10, adaptedSearch.Length))))
                {
                    plane.gameObject.SetActive(true);
                    anyActive = true;
                }
                else
                {
                    plane.gameObject.SetActive(false);
                }
            }

            ToggleEmptyPlane(!anyActive);
        }

        private void EnableAllSubscriptionWindows()
        {
            foreach (var plane in _articlePlanes)
            {
                plane.gameObject.SetActive(true);
            }
            
            _clearSearchButton.gameObject.SetActive(false);
            _searchImage.enabled = true;
            ToggleEmptyPlane(false);
        }

        private void VerifyActivePlanes()
        {
            foreach (var plane in _articlePlanes)
            {
                if (plane.isActiveAndEnabled)
                {
                    ToggleEmptyPlane(false);
                    return;
                }
            }
            
            ToggleEmptyPlane(true);
        }

        private void ToggleEmptyPlane(bool status)
        {
            _emptyPlane.gameObject.SetActive(status);
        }
    }
    
}


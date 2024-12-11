using System;
using Articles;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private SettingsScreenView _view;
    [SerializeField] private VersionView _versionView;
    [SerializeField] private PrivacyPolicyView _privacyPolicyView;
    [SerializeField] private TermsOfUseView _termsOfUseView;
    [SerializeField] private MainScreen _mainScreen;
    [SerializeField] private ArticlesMainScreen _articlesMainScreen;
    
    private string _email = "osmandalbasti54@icloud.com";

    public event Action MainMenuButtonClicked;
    public event Action ArticlesButtonClicked;

    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.FeedbackButtonClicked += ProcessFeedbackButtonClicked;
        _view.VersionButtonClicked += ProcessVersionButtonClicked;
        _view.TermsOfUseButtonClicked += ProcessTermsOfUseButtonClicked;
        _view.PrivacyPolicyButtonClicked += ProcessPrivicyPolicyButtonClicked;
        _view.MenuButtonClicked += ProcessMenuButtonClicked;
        _view.ContactUsButtonClicked += ProcessContactUsButtonClicked;
        _view.ArticleButtonClicked += ProcessArticlesButtonClicked;
        
        _privacyPolicyView.BackButtonClicked += ShowScreen;
        _versionView.BackButtonClicked += ShowScreen;
        _termsOfUseView.BackButtonClicked += ShowScreen;
        
        _mainScreen.OpenSettings += ShowScreen;
        _articlesMainScreen.SettingsClicked += ShowScreen;
    }

    private void OnDisable()
    {
        _view.FeedbackButtonClicked -= ProcessFeedbackButtonClicked;
        _view.VersionButtonClicked -= ProcessVersionButtonClicked;
        _view.TermsOfUseButtonClicked -= ProcessTermsOfUseButtonClicked;
        _view.PrivacyPolicyButtonClicked -= ProcessPrivicyPolicyButtonClicked;
        _view.MenuButtonClicked -= ProcessMenuButtonClicked;
        _view.ContactUsButtonClicked -= ProcessContactUsButtonClicked;
        _view.ArticleButtonClicked -= ProcessArticlesButtonClicked;
        
        _privacyPolicyView.BackButtonClicked -= ShowScreen;
        _versionView.BackButtonClicked -= ShowScreen;
        _termsOfUseView.BackButtonClicked -= ShowScreen;
        
        _mainScreen.OpenSettings -= ShowScreen;
        _articlesMainScreen.SettingsClicked -= ShowScreen;
    }

    private void ProcessMenuButtonClicked()
    {
        MainMenuButtonClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessArticlesButtonClicked()
    {
        ArticlesButtonClicked?.Invoke();
        _view.Disable();
    }

    private void ShowScreen()
    {
        _view.Enable();
    }

    private void ProcessPrivicyPolicyButtonClicked()
    {
        _privacyPolicyView.Enable();
        _view.Disable();
    }

    private void ProcessTermsOfUseButtonClicked()
    {
        _termsOfUseView.Enable();
        _view.Disable();
    }

    private void ProcessVersionButtonClicked()
    {
        _versionView.Enable();
        _view.Disable();
    }

    private void ProcessFeedbackButtonClicked()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }

    private void ProcessContactUsButtonClicked()
    {
        Application.OpenURL("mailto:" + _email + "?subject=Mail to developer");
    }
}

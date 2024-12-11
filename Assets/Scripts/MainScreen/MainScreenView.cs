using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class MainScreenView : MonoBehaviour
{
    [SerializeField] private Button _articlesButton;
    [SerializeField] private Button _settingsButton;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action ArticlesClicked;
    public event Action SettingsClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _articlesButton.onClick.AddListener(OnArticlesClicked);
        _settingsButton.onClick.AddListener(OnSettingsClicked);
    }

    private void OnDisable()
    {
        _articlesButton.onClick.RemoveListener(OnArticlesClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsClicked);
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void OnArticlesClicked() => ArticlesClicked?.Invoke();
    private void OnSettingsClicked() => SettingsClicked?.Invoke();
}

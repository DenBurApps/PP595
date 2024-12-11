using System;
using Articles;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private GameClosedPlane _penaltyClosedPlane;
    [SerializeField] private GameClosedPlane _basketbalClosedPlane;
    [SerializeField] private GameClosedPlane _athleticsClosedPlane;
    
    [SerializeField] private GameOpenPlane _penaltyOpenPlane;
    [SerializeField] private GameOpenPlane _basketbalOpenPlane;
    [SerializeField] private GameOpenPlane _athleticsOpenPlane;

    [SerializeField] private MainScreenView _view;

    [SerializeField] private SettingsScreen _settingsScreen;
    [SerializeField] private ArticlesMainScreen _articlesMainScreen;
    
    public event Action OpenSettings;
    public event Action ArticlesOpen;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnEnable()
    {
        _view.SettingsClicked += OnSettingsOpen;
        _view.ArticlesClicked += OnArticlesOpen;

        _penaltyClosedPlane.OpenButtonClicked += OnPenalatyOpen;
        _basketbalClosedPlane.OpenButtonClicked += OnBasketbalOpen;
        _athleticsClosedPlane.OpenButtonClicked += OnAthleticsOpen;

        _penaltyOpenPlane.CloseButtonClicked += OnPenaltyOpenCloseClicked;
        _basketbalOpenPlane.CloseButtonClicked += OnBasketballOpenCloseClicked;
        _athleticsOpenPlane.CloseButtonClicked += OnAthleticOpenCloseClicked;

        _penaltyOpenPlane.PlayButtonClicked += OnPenaltyLetsPlayClicked;
        _basketbalOpenPlane.PlayButtonClicked += OnBasketbalLetsPlayClicked;
        _athleticsOpenPlane.PlayButtonClicked += OnAthleticLetsPlayClicked;

        _settingsScreen.MainMenuButtonClicked += _view.Enable;
        _articlesMainScreen.GamesClicked += _view.Enable;
        
        SetScoreText(_penaltyOpenPlane.RecordText, "PenaltyScore");
        SetScoreText(_basketbalOpenPlane.RecordText, "BasketballScore");
        SetScoreText(_athleticsOpenPlane.RecordText, "AthleticsScore");
    }

    private void OnDisable()
    {
        _view.SettingsClicked -= OnSettingsOpen;
        _view.ArticlesClicked -= OnArticlesOpen;
        
        _penaltyClosedPlane.OpenButtonClicked -= OnPenalatyOpen;
        _basketbalClosedPlane.OpenButtonClicked -= OnBasketbalOpen;
        _athleticsClosedPlane.OpenButtonClicked -= OnAthleticsOpen;

        _penaltyOpenPlane.CloseButtonClicked -= OnPenaltyOpenCloseClicked;
        _basketbalOpenPlane.CloseButtonClicked -= OnBasketballOpenCloseClicked;
        _athleticsOpenPlane.CloseButtonClicked -= OnAthleticOpenCloseClicked;

        _penaltyOpenPlane.PlayButtonClicked -= OnPenaltyLetsPlayClicked;
        _basketbalOpenPlane.PlayButtonClicked -= OnBasketbalLetsPlayClicked;
        _athleticsOpenPlane.PlayButtonClicked -= OnAthleticLetsPlayClicked;
    }
    
    private void Start()
    {
        _penaltyOpenPlane.Disable();
        _basketbalOpenPlane.Disable();
        _athleticsOpenPlane.Disable();
    }


    private void OnPenalatyOpen()
    {
        _basketbalClosedPlane.SetTransparent();
        _athleticsClosedPlane.SetTransparent();
        _penaltyClosedPlane.Disable();
        _penaltyOpenPlane.Enable();
    }

    private void OnBasketbalOpen()
    {
        _penaltyClosedPlane.SetTransparent();
        _athleticsClosedPlane.SetTransparent();
        _basketbalClosedPlane.Disable();
        _basketbalOpenPlane.Enable();
    }

    private void OnAthleticsOpen()
    {
        _basketbalClosedPlane.SetTransparent();
        _penaltyClosedPlane.SetTransparent();
        _athleticsClosedPlane.Disable();
        _athleticsOpenPlane.Enable();
    }

    private void OnPenaltyOpenCloseClicked()
    {
        _penaltyOpenPlane.Disable();
        _penaltyClosedPlane.Enable();
        _basketbalClosedPlane.SetDefaultColor();
        _athleticsClosedPlane.SetDefaultColor();
        _penaltyClosedPlane.SetDefaultColor();
    }

    private void OnBasketballOpenCloseClicked()
    {
        _basketbalOpenPlane.Disable();
        _basketbalClosedPlane.Enable();
        _basketbalClosedPlane.SetDefaultColor();
        _athleticsClosedPlane.SetDefaultColor();
        _penaltyClosedPlane.SetDefaultColor();
    }

    private void OnAthleticOpenCloseClicked()
    {
        _athleticsOpenPlane.Disable();
        _athleticsClosedPlane.Enable();
        _basketbalClosedPlane.SetDefaultColor();
        _athleticsClosedPlane.SetDefaultColor();
        _penaltyClosedPlane.SetDefaultColor();
    }

    private void OnPenaltyLetsPlayClicked()
    {
        SceneManager.LoadScene("PenaltyKickScene");
    }

    private void OnBasketbalLetsPlayClicked()
    {
        SceneManager.LoadScene("BasketballScene");
    }
    
    private void OnAthleticLetsPlayClicked()
    {
        SceneManager.LoadScene("AthleticsScene");
    }

    private void OnArticlesOpen()
    {
        ArticlesOpen?.Invoke();
        _view.Disable();
    }

    private void OnSettingsOpen()
    {
        OpenSettings?.Invoke();
        _view.Disable();
    }

    private void SetScoreText(TMP_Text text, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            text.text = PlayerPrefs.GetInt(key).ToString();
        }
        else
        {
            text.text = 0.ToString();
        }
    }
    
    //PlayerPrefs set record to every open plane
}

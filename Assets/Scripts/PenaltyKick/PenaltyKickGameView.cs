using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class PenaltyKickGameView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _directionButton;
    [SerializeField] private Button _impactButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Image _fieldImage;
    [SerializeField] private Image _impactField;
    [SerializeField] private Image _impactFilledImage;
    [SerializeField] private Image _background;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _defaultBackgroundColor;
    [SerializeField] private Color _transparentColor;
    [SerializeField] private Color _transparentBackgroundColor;

    private ScreenVisabilityHandler _screenVisabilityHandler;
    
    public event Action ExitButtonClicked;
    public event Action DirectionButtonClicked;
    public event Action ImpactButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitClicked);
        _directionButton.onClick.AddListener(OnDirectionClicked);
        _impactButton.onClick.AddListener(OnImpactClicked);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitClicked);
        _directionButton.onClick.RemoveListener(OnDirectionClicked);
        _impactButton.onClick.RemoveListener(OnImpactClicked);
    }

    public void Enable()
    {
        _fieldImage.color = _defaultColor;
        _impactFilledImage.color = _defaultColor;
        _impactField.color = _defaultColor;
        _background.color = _defaultBackgroundColor;
    }
    
    public void DisableInteractions()
    {
        _fieldImage.color = _transparentColor;
        _impactFilledImage.color = _transparentColor;
        _impactField.color = _transparentColor;
        _background.color = _transparentBackgroundColor;
    }

    public void ToggleDirectionButton(bool status)
    {
        _directionButton.gameObject.SetActive(status);
    }

    public void ToggleImpactButton(bool status)
    {
        _impactButton.gameObject.SetActive(status);
    }

    public void SetScoreText(int score)
    {
        _score.text = score.ToString() + " scores";
    }

    private void OnExitClicked() => ExitButtonClicked?.Invoke();
    private void OnDirectionClicked() => DirectionButtonClicked?.Invoke();
    private void OnImpactClicked() => ImpactButtonClicked?.Invoke();
}

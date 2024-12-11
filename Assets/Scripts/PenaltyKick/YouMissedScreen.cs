using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class YouMissedScreen : MonoBehaviour
{
    [SerializeField] private Button _tryAgainButton;

    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action TryAgainClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainClicked);
    }

    private void OnDisable()
    {
        _tryAgainButton.onClick.RemoveListener(OnTryAgainClicked);
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    private void OnTryAgainClicked() => TryAgainClicked?.Invoke();
}

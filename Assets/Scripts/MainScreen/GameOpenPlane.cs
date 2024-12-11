using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOpenPlane : MonoBehaviour
{
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _closeButton;

    public event Action CloseButtonClicked;
    public event Action PlayButtonClicked;

    public TMP_Text RecordText => _recordText;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetRecord(string record)
    {
        _recordText.text = record;
    }

    private void OnCloseButtonClicked() => CloseButtonClicked?.Invoke();
    private void OnPlayButtonClicked() => PlayButtonClicked?.Invoke();
}

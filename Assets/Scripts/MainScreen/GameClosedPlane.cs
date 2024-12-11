using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GameClosedPlane : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _invisbleColor;

    private Image _image;
    
    public event Action OpenButtonClicked;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _defaultColor = _image.color;
        
        _openButton.onClick.AddListener(OnOpenButtonClicked);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetTransparent()
    {
        _image.color = _invisbleColor;
        _openButton.enabled = false;
    }

    public void SetDefaultColor()
    {
        _image.color = _defaultColor;
        _openButton.enabled = true;
    }

    private void OnOpenButtonClicked()
    {
        OpenButtonClicked?.Invoke();
    }
}

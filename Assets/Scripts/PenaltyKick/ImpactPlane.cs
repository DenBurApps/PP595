using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImpactPlane : MonoBehaviour
{
    [SerializeField] private Image _filledBar;
    [SerializeField] private float _fillingSpeed;

    private IEnumerator _currentCoroutine;

    private void OnEnable()
    {
        _filledBar.fillAmount = 0;
        FillToMax();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public float GetStoppedFilledAmount()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        return _filledBar.fillAmount;
    }

    private void FillToMax()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartFillingToMax();
        StartCoroutine(_currentCoroutine);
    }

    private void FillToMin()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartFillingToMin();
        StartCoroutine(_currentCoroutine);
    }
    
    private IEnumerator StartFillingToMax()
    {
        while (_filledBar.fillAmount < 1)
        {
            _filledBar.fillAmount = Mathf.MoveTowards(_filledBar.fillAmount, 1, _fillingSpeed * Time.deltaTime);
            yield return null;
        }
        
        _filledBar.fillAmount = 1;
        FillToMin();
    }
    
    private IEnumerator StartFillingToMin()
    {
        while (_filledBar.fillAmount > 0)
        {
            _filledBar.fillAmount = Mathf.MoveTowards(_filledBar.fillAmount, 0, _fillingSpeed * Time.deltaTime);

            yield return null;
        }

        _filledBar.fillAmount = 0;
        FillToMax();
    }
}

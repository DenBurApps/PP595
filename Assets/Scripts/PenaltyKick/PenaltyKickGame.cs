using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenaltyKickGame : MonoBehaviour
{
    private const int WaitBeforeRestartTime = 3;
    
    [SerializeField] private Gate _gate;
    [SerializeField] private Ball _ball;
    [SerializeField] private ImpactPlane _impactPlane;
    [SerializeField] private PenaltyKickGameView _view;
    [SerializeField] private YouMissedScreen _youMissedScreen;
    [SerializeField] private Point _point;
    [SerializeField] private LayerMask _gateLayer;
    
    private int _score;

    private IEnumerator _waitAndLostCoroutine;

    private void OnEnable()
    {
        _view.DirectionButtonClicked += DirectionPressed;
        _view.ImpactButtonClicked += ImpactPressed;
        _view.ExitButtonClicked += ExitGame;

        _youMissedScreen.TryAgainClicked += StartGame;
        
        _gate.BallCollided += ProcessGameWin;
        
    }

    private void OnDisable()
    {
        _view.DirectionButtonClicked -= DirectionPressed;
        _view.ImpactButtonClicked -= ImpactPressed;
        _view.ExitButtonClicked -= ExitGame;
        
        _youMissedScreen.TryAgainClicked -= StartGame;

        _gate.BallCollided -= ProcessGameWin;
    }

    private void Start()
    {
        _score = 0;
        StartGame();
    }

    private void StartGame()
    {
        _view.SetScoreText(_score);
        _view.ToggleDirectionButton(true);
        _view.ToggleImpactButton(false);

        _youMissedScreen.Disable();
        _gate.DiactivateGateTrigger();
        _impactPlane.Disable();
        _point.Disable();
        _ball.ReturnToDefaultPosition();
        _ball.Rigidbody2D.isKinematic = true;
        _ball.StartMoving();
        
        _view.Enable();
    }

    private void DirectionPressed()
    {
        _ball.FreezeMovement();
        
        RaycastHit2D ray = Physics2D.Raycast(_ball.Transform.position, _ball.GetDirectionVector(), Mathf.Infinity, _gateLayer);

        if (ray.collider != null)
        {
            _point.Enable();
            _point.SetPosition(ray.point);
            _view.ToggleDirectionButton(false);
            _view.ToggleImpactButton(true);
            _impactPlane.Enable();
            _gate.SetGateAsTrigger();
        }
        else
        {
            ProcessGameLost();
        }
    }

    private void ProcessGameLost()
    {
        _score = 0;
        _view.DisableInteractions();
        _view.ToggleDirectionButton(false);
        _view.ToggleImpactButton(false);
        
        _youMissedScreen.Enable();
    }

    private void ProcessGameWin()
    {
        _score++;
        PrefsScoreSaver.SaveScore("PenaltyScore", _score);

        if (_waitAndLostCoroutine != null)
        {
            StopCoroutine(_waitAndLostCoroutine);
            _waitAndLostCoroutine = null;
        }
        
        StartGame();
    }
    
    private IEnumerator WaitAndProcessGameLost()
    {
        yield return new WaitForSeconds(WaitBeforeRestartTime);
        ProcessGameLost();
    }
    
    private void ImpactPressed()
    {
        if(!_impactPlane.isActiveAndEnabled)
            return;
        
        _view.ToggleImpactButton(false);
        
        float force = _impactPlane.GetStoppedFilledAmount();
        _ball.Rigidbody2D.isKinematic = false;
        _ball.Rigidbody2D.drag = 2f;
        _ball.SetThrowForce(force);

        if (_waitAndLostCoroutine != null)
        {
            StopCoroutine(_waitAndLostCoroutine);
            _waitAndLostCoroutine = null;
        }

        _waitAndLostCoroutine = WaitAndProcessGameLost();
        StartCoroutine(_waitAndLostCoroutine);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
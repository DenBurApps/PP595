using UnityEngine;
using UnityEngine.SceneManagement;

namespace AthleticsRace
{
    public class AthleticsGame : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private CharacterProvider _characterProvider;
        [SerializeField] private ChooseCharacterScreen _chooseCharacterScreen;
        [SerializeField] private CountdownScreen _countdownScreen;
        [SerializeField] private YouMissedScreen _youMissedScreen;
        [SerializeField] private AthleticsGameView _view;
        [SerializeField] private FinishBorder _finishBorder;

        private int _score;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        private void OnEnable()
        {
            _chooseCharacterScreen.CharacterChosen += SelectedCharacter;
            _countdownScreen.GameStarted += StartNewGame;
            _view.ExitClicked += ExitGame;
            _youMissedScreen.TryAgainClicked += TryAgain;
            _finishBorder.Finished += ProcessFinish;

        }

        private void OnDisable()
        {
            _chooseCharacterScreen.CharacterChosen -= SelectedCharacter;
            _countdownScreen.GameStarted -= StartNewGame;
            _view.ExitClicked -= ExitGame;
            
            _youMissedScreen.TryAgainClicked -= TryAgain;
            
            _finishBorder.Finished -= ProcessFinish;
        }

        private void Start()
        {
            _chooseCharacterScreen.EnableScreen();
            _countdownScreen.DisableScreen();
            _player.DisableInput();
            _enemy.DisableMovement();
            _view.SetTranparent();
            _player.DisableSprite();
            _enemy.DisableSprite();
            _youMissedScreen.Disable();
            _score = 0;
        }


        private void SelectedCharacter(Character character)
        {
            _player.SetCharacterSprite(character.Sprite);

            Character enemyCharacter = _characterProvider.GetAnotherCharacter(character);
            _enemy.SetCharacterSprite(enemyCharacter.Sprite);

            _chooseCharacterScreen.DisableScreen();
            _countdownScreen.EnableScreen();
            _view.SetDefaultColor();
            _player.EnableSprite();
            _enemy.EnableSprite();
        }

        private void StartNewGame()
        {
            _countdownScreen.DisableScreen();
            
            _view.SetDefaultColor();
            _view.SetScoreText(_score);
            _enemy.ReturnToDefault();
            _player.ReturnToDefault();
            _player.EnableInput();
            _enemy.EnableMovement();
            _finishBorder.EnableTrigger();
        }

        private void ProcessFinish(IInteractable interactable)
        {
            if (interactable is Player)
            {
                ProcessGameWin();
            }
            else if(interactable is Enemy)
            {
                ProcessGameLost();
            }
        }

        private void ProcessGameWin()
        {
            _score++;
            
            PrefsScoreSaver.SaveScore("AthleticsScore", _score);
            
            _player.DisableInput();
            _enemy.DisableMovement();
            _countdownScreen.EnableScreen();
        }

        private void ProcessGameLost()
        {
            _score = 0;
            
            _youMissedScreen.Enable();
        }

        private void TryAgain()
        {
            _youMissedScreen.Disable();
            _countdownScreen.EnableScreen();
        }

        private void ExitGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
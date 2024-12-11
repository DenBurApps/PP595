using System;
using UnityEngine;
using UnityEngine.UI;

namespace AthleticsRace
{
    public class ChooseCharacterScreen : MonoBehaviour
    {
        [SerializeField] private Button[] _characterButtons;
        [SerializeField] private CharacterTypes[] _characterTypes;
        [SerializeField] private CharacterProvider _characterProvider; 

        public event Action<Character> CharacterChosen;

        private void OnEnable()
        {
            for (int i = 0; i < _characterButtons.Length; i++)
            {
                int index = i;
                _characterButtons[i].onClick.AddListener(() => ChooseCharacter(_characterTypes[index]));
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _characterButtons.Length; i++)
            {
                int index = i;
                _characterButtons[i].onClick.RemoveListener(() => ChooseCharacter(_characterTypes[index]));
            }
        }

        public void EnableScreen()
        {
            gameObject.SetActive(true);
        }

        public void DisableScreen()
        {
            gameObject.SetActive(false);
        }

        private void ChooseCharacter(CharacterTypes type)
        {
            Character selectedCharacter = _characterProvider.GetCharacterByType(type);
            CharacterChosen?.Invoke(selectedCharacter);
        }
    }
    
}


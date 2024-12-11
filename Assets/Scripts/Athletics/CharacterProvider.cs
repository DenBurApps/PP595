using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AthleticsRace
{
    public class CharacterProvider : MonoBehaviour
    {
        [SerializeField] private Character[] _characters;
        private Dictionary<CharacterTypes, Character> _characterDictionary;

        private void Awake()
        {
            _characterDictionary = new Dictionary<CharacterTypes, Character>();
            foreach (var character in _characters)
            {
                if (!_characterDictionary.ContainsKey(character.Type))
                {
                    _characterDictionary[character.Type] = character;
                }
            }
        }
        
        public Character GetCharacterByType(CharacterTypes type)
        {
            _characterDictionary.TryGetValue(type, out var character);
            return character;
        }

        public Character GetAnotherCharacter(Character character)
        {
            return _characterDictionary.Values.FirstOrDefault(c => c != character);
        }
    }

    [Serializable]
    public class Character
    {
        public CharacterTypes Type;
        public Sprite Sprite;
    }
    
    public enum CharacterTypes
    {
        First,
        Second,
        Third
    }
}

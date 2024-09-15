using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersDB", menuName = "Create Characters Database")]
public class CharactersDB : ScriptableObject {
    [SerializeField] private List<CharacterDB> _characterDatabases = null;

    public List<CharacterDB> CharacterDatabases { get => _characterDatabases; set => _characterDatabases = value; }

    public CharacterDB GetCharacter(int index) {
        return _characterDatabases[index];
    }
}

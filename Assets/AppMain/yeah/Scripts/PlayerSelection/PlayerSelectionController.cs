using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionController : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer = null;

    private void Start() {
        UpdatePlayerCharacter(GameDirector.Instance.PlayerCharacterIndex);
    }

    public void UpdatePlayerCharacter(int characterIndex) {
        GameDirector.Instance.PlayerCharacterIndex = characterIndex;
        
        var character = _charactersDB.GetCharacter(characterIndex);
        _playerSpriteRenderer.sprite = character.CharacterSprites[2];
    }
}

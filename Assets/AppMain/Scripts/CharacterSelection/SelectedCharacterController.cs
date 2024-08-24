using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterController : MonoBehaviour {
    [SerializeField] private CharacterDatabase _characterDatabase;

    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;

    private void Start() {
        UpdatePlayerCharacter(GameDirector.Instance.PlayerCharacterIndex);
        UpdateEnemyCharacter(GameDirector.Instance.EnemyCharacterIndex);
    }

    public void UpdatePlayerCharacter(int index) {
        var character = _characterDatabase.GetCharacter(index);
        _playerSpriteRenderer.sprite = character.CharacterSprites[2];
    }
    
    private void UpdateEnemyCharacter(int index) {
        var character = _characterDatabase.GetCharacter(index);
        _enemySpriteRenderer.sprite = character.CharacterSprites[2];
    }
}

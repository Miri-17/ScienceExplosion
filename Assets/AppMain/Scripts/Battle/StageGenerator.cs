using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB;
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private SpriteRenderer _enemy;
    [SerializeField] private Image _bgImage;

    private void Start() {
        if (GameDirector.Instance == null) {
            Debug.Log("GameDirecotr is missing!");
            Destroy(this);
            return;
        }

        var playerCharacter = _charactersDB.GetCharacter(GameDirector.Instance.PlayerCharacterIndex);
        var enemyCharacter = _charactersDB.GetCharacter(GameDirector.Instance.EnemyCharacterIndex);
        _player.sprite = playerCharacter.CharacterSprites[1];
        _enemy.sprite = enemyCharacter.CharacterSprites[1];
        _bgImage.sprite = enemyCharacter.PlaceSprites[1];
    }
}

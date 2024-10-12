using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB;
    [SerializeField] private Image _bgImage;
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private SpriteRenderer _enemy;
    // [SerializeField] private Image _labelImage;
    // [SerializeField] private Image _characterImage;
    // [SerializeField] private SpriteRenderer _playerLive2D;
    // [SerializeField] private SpriteRenderer _enemyLive2D;
    // [SerializeField] private List<Image> _playerBackgrounds;
    // [SerializeField] private List<Image> _enemyBackgrounds;

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
        // _labelImage.color = playerCharacter.UniqueColor;
        // _characterImage.sprite = playerCharacter.CharacterSprites[2];

        // _playerLive2D.sprite = playerCharacter.CharacterSprites[2];
        // _enemyLive2D.sprite = enemyCharacter.CharacterSprites[2];
        // _playerBackgrounds[0].color = playerCharacter.UniqueColor;
        // _playerBackgrounds[1].color = playerCharacter.UniqueColor;
        // _enemyBackgrounds[0].color = enemyCharacter.UniqueColor;
        // _enemyBackgrounds[1].color = enemyCharacter.UniqueColor;
    }
}

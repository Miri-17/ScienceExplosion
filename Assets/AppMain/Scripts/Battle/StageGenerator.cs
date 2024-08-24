using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour {
    [SerializeField] private CharacterDatabase _characterDatabase;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private SpriteRenderer _enemy;

    private void Start() {
        if (GameDirector.Instance != null) {
            var playerCharacter = _characterDatabase.GetCharacter(GameDirector.Instance.PlayerCharacterIndex);
            var enemyCharacter = _characterDatabase.GetCharacter(GameDirector.Instance.EnemyCharacterIndex);
            _player.sprite = playerCharacter.CharacterSprites[1];
            _enemy.sprite = enemyCharacter.CharacterSprites[1];
            _backgroundImage.sprite = enemyCharacter.PlaceSprites[1];
        } else {
            Debug.Log("GameDirecotr is missing!");
            Destroy(this);
            return;
        }

    }
}

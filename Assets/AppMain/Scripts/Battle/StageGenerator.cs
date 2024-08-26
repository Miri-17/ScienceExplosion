using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour {
    [SerializeField] private CharacterDatabase _characterDatabase;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private SpriteRenderer _enemy;
    [SerializeField] private Image _labelImage;
    [SerializeField] private Image _characterImage;

    private void Start() {
        if (GameDirector.Instance != null) {
            var playerCharacter = _characterDatabase.GetCharacter(GameDirector.Instance.PlayerCharacterIndex);
            var enemyCharacter = _characterDatabase.GetCharacter(GameDirector.Instance.EnemyCharacterIndex);
            _player.sprite = playerCharacter.CharacterSprites[1];
            _enemy.sprite = enemyCharacter.CharacterSprites[1];
            _backgroundImage.sprite = enemyCharacter.PlaceSprites[1];
            _labelImage.color = playerCharacter.UniqueColor;
            _characterImage.sprite = playerCharacter.CharacterSprites[2];
        } else {
            Debug.Log("GameDirecotr is missing!");
            Destroy(this);
            return;
        }

    }
}

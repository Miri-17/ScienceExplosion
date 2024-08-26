using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterController : MonoBehaviour {
    #region
    [SerializeField] private CharacterDatabase _characterDatabase;

    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;

    [SerializeField] private Image _unmask = null;
    [SerializeField] private Image _screen = null;
    #endregion

    private void Start() {
        UpdatePlayerCharacter(GameDirector.Instance.PlayerCharacterIndex);
        UpdateEnemyCharacter(GameDirector.Instance.EnemyCharacterIndex);
    }

    public void UpdatePlayerCharacter(int index) {
        var character = _characterDatabase.GetCharacter(index);
        _playerSpriteRenderer.sprite = character.CharacterSprites[2];

        _unmask.sprite = character.NationMark;
        _screen.color = character.UniqueColor;
    }
    
    public void UpdateEnemyCharacter(int index) {
        var character = _characterDatabase.GetCharacter(index);
        _enemySpriteRenderer.sprite = character.CharacterSprites[2];
    }
}

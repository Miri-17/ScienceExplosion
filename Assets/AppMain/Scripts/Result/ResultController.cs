using UnityEngine;

public class ResultController : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private void Start() {
        UpdateCharacter(GameDirector.Instance.PlayerCharacterIndex);
    }

    private void UpdateCharacter(int index) {
        var player = _charactersDB.GetCharacter(index);
        _playerSpriteRenderer.sprite = player.CharacterSprites[2];
    }
}

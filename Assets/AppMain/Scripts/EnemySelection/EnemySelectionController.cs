using UnityEngine;

public class EnemySelectionController : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer = null;

    private void Start() {
        // ここのインデックス以外、MapControllerと同じ
        UpdateCharacter(GameDirector.Instance.PlayerCharacterIndex);
    }

    // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも
    private void UpdateCharacter(int index) {
        var character = _charactersDB.GetCharacter(index);

        _characterSpriteRenderer.sprite = character.CharacterSprites[1];
    }
}

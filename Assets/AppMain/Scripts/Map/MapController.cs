using DG.Tweening;
using UnityEngine;

public class MapController : MonoBehaviour {
    // [SerializeField] private Transform _character = null;
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer = null;

    private void Start() {
        // ここのインデックス以外、EnemySelectionControllerと同じ.
        UpdateCharacter(GameDirector.Instance.SelectedCharacterIndex);
    }

    // private void Update() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         _character.DOLocalMove(new Vector3(tapPoint.x, tapPoint.y, 80f), 1f);
    //     }
    // }

    // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも.
    private void UpdateCharacter(int index) {
        var character = _charactersDB.GetCharacter(index);

        _characterSpriteRenderer.sprite = character.CharacterSprites[1];
    }
}

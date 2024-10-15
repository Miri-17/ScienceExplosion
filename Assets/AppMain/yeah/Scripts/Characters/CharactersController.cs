using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ほとんどPlayerSelectionControllerと同じ->統合
public class CharactersController : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer = null;

    /// <summary>
    /// 選択中のキャラ情報を保持した変数
    /// </summary>
    public CharacterDB Character = null;

    private void Start() {
        UpdatePlayerCharacter(GameDirector.Instance.SelectedCharacterIndex);
    }

    public void UpdatePlayerCharacter(int characterIndex) {
        Character = _charactersDB.GetCharacter(characterIndex);
        _playerSpriteRenderer.sprite = Character.CharacterSprites[2];
    }
}

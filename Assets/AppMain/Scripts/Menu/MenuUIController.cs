using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuUIController : MonoBehaviour {
    [SerializeField] private CharactersDB _charactersDB;
    [SerializeField] private RectTransform _flaskImage;
    [SerializeField] private RectTransform _chargeImage;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer;
    // [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _background;

    private void Start() {
        _flaskImage.DOAnchorPos(new Vector2(-4f, 23f), 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_flaskImage.gameObject);
        
        _chargeImage.localScale = Vector3.one * 0.8f;
        _chargeImage.DOScale(1f, 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_chargeImage.gameObject);
        
        UpdateCharacter(GameDirector.Instance.SelectedCharacterIndex);
        var character = _charactersDB.GetCharacter(GameDirector.Instance.SelectedPlaceIndex);
        _background.sprite = character.PlaceSprites[0];
    }

    // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも
    // CharacterのUIController.csにも使用されている。
    private void UpdateCharacter(int index) {
        var character = _charactersDB.GetCharacter(index);
        _characterSpriteRenderer.sprite = character.CharacterSprites[0];
        // _nameText.text = character.Name;
    }
}

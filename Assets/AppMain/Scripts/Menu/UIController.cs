using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace ScienceExplosion.Menu {
    public class UIController : MonoBehaviour {
        [SerializeField] private RectTransform _flaskImage;

        [SerializeField] private CharacterDatabase _characterDatabase;
        [SerializeField] private Image _background;
        [SerializeField] private Image _nationMark;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _placeText;
        [SerializeField] private SpriteRenderer _characterSpriteRenderer;

        private void Start() {
            _flaskImage.DOAnchorPos(new Vector2(-10.1f, 38.0f), 1.0f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_flaskImage.gameObject);
            
            UpdateCharacter(GameDirector.Instance.SelectedCharacterIndex);
        }

        // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも
        // CharacterのUIController.csにも使用されている。
        private void UpdateCharacter(int index) {
            var character = _characterDatabase.GetCharacter(index);
            _background.sprite = character.PlaceSprite;
            _nationMark.sprite = character.NationMark;
            if (index == 8) {
                _nameText.text = character.Name.Substring(0, 6);
            } else {
                _nameText.text = character.Name;
            }
            _placeText.text = character.Place;
            _characterSpriteRenderer.sprite = character.CharacterSprite;
        }
    }
}

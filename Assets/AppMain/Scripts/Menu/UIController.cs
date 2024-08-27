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
        [SerializeField] private Image _settingsBackground = null;
        [SerializeField] private List<Color> _settingsColor = null;

        private void Start() {
            _flaskImage.DOAnchorPos(new Vector2(-10.1f, 38.0f), 1.0f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_flaskImage.gameObject);
            
            UpdateCharacter(GameDirector.Instance.SelectedCharacterIndex);
            var character = _characterDatabase.GetCharacter(GameDirector.Instance.SelectedBackgroundIndex);
            _background.sprite = character.PlaceSprites[0];

            _settingsBackground.color = _settingsColor[GameDirector.Instance.SelectedCharacterIndex];
        }

        // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも
        // CharacterのUIController.csにも使用されている。
        private void UpdateCharacter(int index) {
            var character = _characterDatabase.GetCharacter(index);
            _nationMark.sprite = character.NationMark;
            _nameText.text = character.Name;
            _placeText.text = character.AffiliationEnglish;
            _characterSpriteRenderer.sprite = character.CharacterSprites[0];
        }
    }
}

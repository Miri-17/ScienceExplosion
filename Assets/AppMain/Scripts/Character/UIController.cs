using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScienceExplosion.Character {
    public class UIController : MonoBehaviour
    {
        #region
        [SerializeField] private CharacterDatabase _characterDatabase;

        [SerializeField] private SpriteRenderer _characterSpriteRenderer;
        [SerializeField] private TextMeshProUGUI _professionText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _skillText;
        [SerializeField] private TextMeshProUGUI _explosionText;
        [SerializeField] private TextMeshProUGUI _placeText;
        [SerializeField] private TextMeshProUGUI _affiliationText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        
        [SerializeField] private List<Button> _characterButtons;
        [SerializeField] private Button _settingButton;
        [SerializeField] private List<Sprite> _settingButtonSprites;
        [SerializeField] private Button _backButton;

        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private int _currentIndex = 0;
        private Image _settingButtonImage = null;
        private bool _isChangeScene = false;

        private void Start() {
            // UpdateCharacter()より下にするとエラーになるので注意してください
            _settingButtonImage = _settingButton.GetComponent<Image>();
            UpdateCharacter(GameDirector.Instance.CharactersFirstIndex);

            for (var i = 0; i < _characterButtons.Count; i++) {
                var index = i;
                _characterButtons[i].onClick.AddListener(() => UpdateCharacter(index));
            }
            _settingButton.onClick.AddListener(() => SettingCharacter());
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }
        
        private void UpdateCharacter(int index) {
            var character = _characterDatabase.GetCharacter(index);
            
            _currentIndex = index;
            
            _professionText.text = character.Profession;
            _nameText.text = character.Name;
            _nameText.color = character.UniqueColor;
            // Dr. Pだけフォントサイズを上げる
            if (index == 0)
                _nameText.fontSize = 160;
            else
                _nameText.fontSize = 120;
            _skillText.text = character.Skill;
            _explosionText.text = character.Explosion;
            _placeText.text = character.Place;
            _affiliationText.text = character.AffiliationJapanese;
            _characterSpriteRenderer.sprite = character.CharacterSprites[2];

            if (index == GameDirector.Instance.SelectedCharacterIndex) {
                _settingButtonImage.sprite = _settingButtonSprites[1];
                _settingButton.enabled = false;
            } else {
                _settingButtonImage.sprite = _settingButtonSprites[0];
                _settingButton.enabled = true;
            }
        }

        private void SettingCharacter() {
            GameDirector.Instance.SelectedCharacterIndex = _currentIndex;
            _settingButtonImage.sprite = _settingButtonSprites[1];
            _settingButton.enabled = false;
        }

        private void OnBackButtonClicked() {
             if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        private IEnumerator GoBackToScene() {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene("Characters");
        }
    }
}

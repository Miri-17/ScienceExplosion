using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

// TODO プレイヤーテロップ関係
// TODO 警告文を出すように変更
namespace ScienceExplosion.CharacterSelection {
    public class UIController : MonoBehaviour {
        private readonly float SCALE_DURATION = 0.5f;
        private bool _isChangeScene = false;

        #region
        [SerializeField] private CharacterDatabase _characterDatabase = null;
        [SerializeField] private SelectedCharacterController _selectedCharacterController = null;
        [SerializeField] private List<Image> _characterButtonImages = null;
        [SerializeField] private List<Button> _characterButtons = null;
        [SerializeField] private Image _namePlate = null;
        [SerializeField] private TextMeshProUGUI _nameText = null;

        // [SerializeField] private List<Image> _playerTelops = null;

        [SerializeField] private Button _backButton = null;
        [SerializeField] private Button _readyButton = null;
        [SerializeField] private RectTransform _unmask = null;
        [SerializeField] private Image _screen = null; 
        #endregion

        private void Start() {
            foreach (var characterButtonImage in _characterButtonImages) {
                characterButtonImage.alphaHitTestMinimumThreshold = 1;
            }

            var character = _characterDatabase.GetCharacter(GameDirector.Instance.PlayerCharacterIndex);
            _namePlate.color = character.UniqueColor;
            _nameText.text = character.Name;

            _screen.enabled = false;

            // for (var i = 0; i < _playerTelops.Count; i++)
            //     _playerTelops[i].enabled = false;

            for (var i = 0; i < _characterButtons.Count; i++) {
                var index = i;
                _characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
            }
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            _readyButton.onClick.AddListener(() => OnReadyButtonClicked());
        }

        private void OnCharacterButtonClicked(int index) {
            var character = _characterDatabase.GetCharacter(index);

            GameDirector.Instance.PlayerCharacterIndex = index;
            _selectedCharacterController.UpdatePlayerCharacter(GameDirector.Instance.PlayerCharacterIndex);

            _namePlate.color = character.UniqueColor;
            _nameText.text = character.Name;

            // _playerTelops[_playerIndex].enabled = false;
            // _playerIndex = index;
            
            _unmask.GetComponent<Image>().sprite = character.NationMark;
            _screen.color = character.UniqueColor;


            // _playerTelops[_playerIndex].enabled = true;
            
            // TODO マジックナンバーでなくす（enum？）
            // _characterSpriteRenderer.sprite = character.CharacterSprites[2];
        }

        private void OnBackButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        // 警告文を出すように変更
        private void OnReadyButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoToBattle());
        }

        private IEnumerator GoBackToScene() {

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene("ModeSelection");
        }

        private IEnumerator GoToBattle() {
            IrisOut();

            yield return new WaitForSeconds(0.5f);
            
            Debug.Log("Go to Battle");
            SceneManager.LoadScene("Battle");
        }

        private void IrisOut() {
            _screen.enabled = true;
            _unmask.DOScale(new Vector3(0, 0, 0), SCALE_DURATION)
                .SetEase(Ease.InBack)
                .SetLink(_unmask.gameObject);
        }
    }
}

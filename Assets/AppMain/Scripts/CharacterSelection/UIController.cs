using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

// 警告文を出すように変更
namespace ScienceExplosion.CharacterSelection {
    public class UIController : MonoBehaviour {
        #region
        [SerializeField] private CharacterDatabase _characterDatabase;
        [SerializeField] private List<Button> _characterButtons = null;
        [SerializeField] private TextMeshProUGUI _nameText = null;
        [SerializeField] private Image _namePlate = null;
        [SerializeField] private List<Image> _playerTelops = null;
        // [SerializeField] private SpriteRenderer _characterSpriteRenderer = null;
        [SerializeField] private Button _exitButton = null;
        [SerializeField] private Button _startButton = null;
        [SerializeField] private RectTransform _unmask = null;
        [SerializeField] private Image _screen = null; 
        #endregion

        private int _playerIndex = 0;
        private readonly float SCALE_DURATION = 0.5f;
        private bool _isChangeScene = false;

        private void Start() {
            _screen.enabled = false;
            for (var i = 0; i < _playerTelops.Count; i++)
                _playerTelops[i].enabled = false;

            // 最初は必ずDr. P
            UpdateCharacter(_playerIndex);

            for (var i = 0; i < _characterButtons.Count; i++) {
                var index = i;
                _characterButtons[i].onClick.AddListener(() => UpdateCharacter(index));
            }
            _exitButton.onClick.AddListener(() => OnExitButtonClicked());
            _startButton.onClick.AddListener(() => OnStartButtonClicked());
        }

        private void UpdateCharacter(int index) {
            var character = _characterDatabase.GetCharacter(index);

            _playerTelops[_playerIndex].enabled = false;
            _playerIndex = index;
            
            _unmask.GetComponent<Image>().sprite = character.NationMark;
            _screen.color = character.UniqueColor;

            _namePlate.color = character.UniqueColor;
            _nameText.text = character.Name;
            _nameText.text = character.Name;

            _playerTelops[_playerIndex].enabled = true;
            
            // TODO マジックナンバーでなくす（enum？）
            // _characterSpriteRenderer.sprite = character.CharacterSprites[2];
        }

        private void OnExitButtonClicked() {
            Debug.Log("exit");
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        // 警告文を出すように変更
        private void OnStartButtonClicked() {
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

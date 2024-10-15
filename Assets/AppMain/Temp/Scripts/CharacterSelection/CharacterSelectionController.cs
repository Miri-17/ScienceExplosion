using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Timeline;

public class CharacterSelectionController : MonoBehaviour {
    private bool _isSceneTransitioning = false;
    private bool _isPlayerSelectionMode = true;
    private int _previousCharacterIndex = 0;

    #region
    [SerializeField] private SelectedCharacterController _selectedCharacterController = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _goToEnemySelectionButton = null;
    [SerializeField] private Button _goBackToPlayerSelectionButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    #endregion 

    private void Start() {
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        _goToEnemySelectionButton.onClick.AddListener(() => OnGoToEnemySelectionButtonClicked());
        _goBackToPlayerSelectionButton.onClick.AddListener(() => OnGoBackToPlayerSelectionButtonClicked());
        for (var i = 0; i < _characterButtons.Count; i++) {
            var index = i;
            _characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
        }

        _previousCharacterIndex = GameDirector.Instance.PlayerCharacterIndex;
        _characterButtons[GameDirector.Instance.PlayerCharacterIndex].interactable = false;
        _selectedCharacterController.UpdatePlayerCharacter(GameDirector.Instance.PlayerCharacterIndex);

        PlayTimeline(0);
    }

    private void OnBackButtonClicked() {
        if (_isSceneTransitioning) return;

        _isSceneTransitioning = true;
        StartCoroutine(GoBackToScene((float)_timelineAssets[1].duration));
    }

    private IEnumerator GoBackToScene(float duration) {
        PlayTimeline(1);

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene("ModeSelection");
    }

    private void OnGoToEnemySelectionButtonClicked() {
        _isPlayerSelectionMode = false;

        // 前のキャラクターインデックスを今の敵キャラクターにすることで
        // 初めて敵を選択した時に、選択したプレイヤーキャラが選べる状態になるのを防ぐ
        _previousCharacterIndex = GameDirector.Instance.EnemyCharacterIndex;
        _characterButtons[GameDirector.Instance.EnemyCharacterIndex].interactable = false;
        PlayTimeline(2);
    }
    
    private void OnGoBackToPlayerSelectionButtonClicked() {
        _isPlayerSelectionMode = true;

        // 前のキャラクターインデックスを今のプレイヤーキャラクターにすることで
        // 初めてプレイヤーを選択した時に、選択していたプレイヤーキャラが選べない状態になるのを防ぐ
        _previousCharacterIndex = GameDirector.Instance.PlayerCharacterIndex;
        _characterButtons[GameDirector.Instance.EnemyCharacterIndex].interactable = true;
        PlayTimeline(3);
    }

    private void OnCharacterButtonClicked(int index) {
        if (_isPlayerSelectionMode) {
            GameDirector.Instance.PlayerCharacterIndex = index;

            _characterButtons[index].interactable = false;
            if (index == GameDirector.Instance.EnemyCharacterIndex) {
                if (index != 0)
                    GameDirector.Instance.EnemyCharacterIndex = 0;
                else
                    GameDirector.Instance.EnemyCharacterIndex = 1;
            }

            _selectedCharacterController.UpdateEnemyCharacter(GameDirector.Instance.EnemyCharacterIndex);
            _selectedCharacterController.UpdatePlayerCharacter(index);
        } else {
            GameDirector.Instance.EnemyCharacterIndex = index;

            _characterButtons[index].interactable = false;

            _selectedCharacterController.UpdateEnemyCharacter(GameDirector.Instance.EnemyCharacterIndex);
        }

        _characterButtons[_previousCharacterIndex].interactable = true;
        _previousCharacterIndex = index;
    }

    private void PlayTimeline(int index) {
        if (index < 0 || index >= _timelineAssets.Count)
            return;
        
        _playableDirector.Play(_timelineAssets[index]);
    }
}

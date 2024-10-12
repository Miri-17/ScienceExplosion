using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ModeSelectionController : MonoBehaviour {
    private bool _isChangeScene = false;

    #region
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _playAloneButton;
    [SerializeField] private Button _playTwoButton; // TODO ネットワークのこと学んだら変更あり
    // [SerializeField] private PlayableDirector _playableDirector = null;
    // [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    #endregion

    private void Start() {
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        _playAloneButton.onClick.AddListener(() => OnPlayAloneButtonClicked());
        _playTwoButton.onClick.AddListener(() => OnPlayTwoButtonClicked());

        // PlayTimeline(0);
    }

    private void OnBackButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(ChangeScene(1, "Menu"));
        ChangeScene(1, "Menu");
    }

    private void OnPlayAloneButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(ChangeScene(2, "CharacterSelection"));
        ChangeScene(2, "CharacterSelection");
    }

    private void OnPlayTwoButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(ChangeScene(2, "Netcode"));
        ChangeScene(2, "Netcode");
    }
    
    // private IEnumerator ChangeScene(int index, string nextScene) {
    private void ChangeScene(int index, string nextScene) {
        // PlayTimeline(index);

        // yield return new WaitForSeconds((float)_timelineAssets[index].duration);

        SceneManager.LoadScene(nextScene);
    }

    // private void PlayTimeline(int index) {
    //     if (index < 0 || index >= _timelineAssets.Count)
    //         return;
        
    //     _playableDirector.Play(_timelineAssets[index]);
    // }
}

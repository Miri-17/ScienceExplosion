using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    private bool _isChangeScene = false;

    #region
    [SerializeField] private Button _charactersButton;
    [SerializeField] private Button _cutscenesButton;
    [SerializeField] private Button _chargeButton;
    [SerializeField] private Button _soundTrackButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    #endregion

    private void Start() {
        _charactersButton.onClick.AddListener(() => OnCharactersButtonClicked());
        _cutscenesButton.onClick.AddListener(() => OnCutscenesButtonClicked());
        _chargeButton.onClick.AddListener(() => OnChargeButtonClicked());
        _soundTrackButton.onClick.AddListener(() => OnSoundTrackButtonClicked());
        _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked());

        PlayTimeline(0);
    }

    private void OnCharactersButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(GoToNextScene("Characters"));
        StartCoroutine(ChangeScene(1, "Characters"));
    }

    private void OnCutscenesButtonClicked() {
        if (_isChangeScene) return;
        

        _isChangeScene = true;
        // StartCoroutine(GoToNextScene("Cutscenes"));
        StartCoroutine(ChangeScene(1, "Cutscenes"));
    }

    private void OnChargeButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(GoToNextScene("ModeSelection"));
        StartCoroutine(ChangeScene(1, "ModeSelection"));
    }

    private void OnSoundTrackButtonClicked() {
        if (_isChangeScene) return;

        _isChangeScene = true;
        // StartCoroutine(GoToNextScene("SoundTrack"));
        StartCoroutine(ChangeScene(1, "SoundTrack"));
    }

    private void OnSettingsButtonClicked() {
        if (_isChangeScene || _settingsPanel.activeSelf) return;

        _settingsPanel.SetActive(true);
    }

    private IEnumerator ChangeScene(int index, string nextScene) {
        PlayTimeline(index);

        yield return new WaitForSeconds((float)_timelineAssets[index].duration);

        SceneManager.LoadScene(nextScene);
    }

    private void PlayTimeline(int index) {
        if (index < 0 || index >= _timelineAssets.Count)
            return;
        
        _playableDirector.Play(_timelineAssets[index]);
    }
}

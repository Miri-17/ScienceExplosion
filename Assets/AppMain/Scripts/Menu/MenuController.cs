using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    private bool _isChangingScene = false;
    private CancellationToken _token = default;

    #region
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _hideUIButton = null;
    [Header("0...Characters, 1...Map, 2...Cutscenes, 3...Audio")]
    [SerializeField] private List<Button> _nextSceneButtons = null;
    [SerializeField] private List<string> _nextSceneNames = new List<string> { "Characters", "Map", "Cutscenes", "Audio" };
    [SerializeField] private Button _chargeButton = null;

    [SerializeField] private GameObject _settingsPanel = null;
    [SerializeField] private GameObject _loadingPanel = null;
    // [SerializeField] private PlayableDirector _playableDirector = null;
    // [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        ChangeAlphaHitThreshold(_settingsButton, 1.0f);
        ChangeAlphaHitThreshold(_hideUIButton, 1.0f);
        foreach(Button nextSceneButton in _nextSceneButtons) {
            ChangeAlphaHitThreshold(nextSceneButton, 1.0f);
        }
        ChangeAlphaHitThreshold(_chargeButton, 1.0f);

        _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked());
        _hideUIButton.onClick.AddListener(() => OnHideUIButtonClicked());
         for (var i = 0; i < _nextSceneButtons.Count; i++) {
            var index = i;
            _nextSceneButtons[i].onClick.AddListener(() => OnNextSceneButtonClicked(index));
        }
        _chargeButton.onClick.AddListener(() => OnChargeButtonClicked());

        // PlayTimeline(0);
    }

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    private void OnSettingsButtonClicked() {
        if (_isChangingScene || _settingsPanel.activeSelf) return;

        _settingsPanel.SetActive(true);
    }

    private void OnHideUIButtonClicked() {
        if (_isChangingScene) return;

        // UIの表示・非表示処理
    }

    private void OnNextSceneButtonClicked(int index) {
        if (_isChangingScene) return;

        _isChangingScene = true;
        // TODO durationの変更
        GoNextSceneAsync(0, _nextSceneNames[index], false).Forget();
    }
    
    private void OnChargeButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        // StartCoroutine(ChangeScene(1, "ModeSelection"));
        // TODO durationの変更
        GoNextSceneAsync(0, "ModeSelection", true).Forget();
    }

    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時
        if (isShowLoadingPanel) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // private IEnumerator ChangeScene(int index, string nextScene) {
    //     PlayTimeline(index);

    //     yield return new WaitForSeconds((float)_timelineAssets[index].duration);

    //     SceneManager.LoadScene(nextScene);
    // }

    // private void PlayTimeline(int index) {
    //     if (index < 0 || index >= _timelineAssets.Count)
    //         return;
        
    //     _playableDirector.Play(_timelineAssets[index]);
    // }
}

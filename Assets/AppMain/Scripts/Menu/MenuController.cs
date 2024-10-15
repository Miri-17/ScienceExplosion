using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour {
    #region
    private bool _isChangingScene = false;
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    // TODO 完全に実装したら消す
    private List<string> _warningTexts = new List<string>() {
        "UIの非表示はまだ未実装です。",
        "サウンドはまだ見られません。",
    };
    #endregion

    #region
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _hideUIButton = null;
    [Header("0...Characters, 1...Map, 2...Cutscenes, 3...Audio")]
    [SerializeField] private List<Button> _nextSceneButtons = null;
    [SerializeField] private List<string> _nextSceneNames = new List<string> { "Characters", "Map", "Cutscenes", "Audio" };
    [SerializeField] private Button _chargeButton = null;
    [SerializeField] private GameObject _settingsPanel = null;
    [SerializeField] private GameObject _loadingPanel = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        ChangeAlphaHitThreshold(_settingsButton, 1.0f);
        ChangeAlphaHitThreshold(_hideUIButton, 1.0f);
        foreach(Button nextSceneButton in _nextSceneButtons) {
            ChangeAlphaHitThreshold(nextSceneButton, 1.0f);
        }
        ChangeAlphaHitThreshold(_chargeButton, 1.0f);

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked());
        _hideUIButton.onClick.AddListener(() => OnHideUIButtonClicked());
         for (var i = 0; i < _nextSceneButtons.Count; i++) {
            var index = i;
            _nextSceneButtons[i].onClick.AddListener(() => OnNextSceneButtonClicked(index));
        }
        _chargeButton.onClick.AddListener(() => OnChargeButtonClicked());

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked());
    }

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    // TODO 完全に実装したら消す
    private void OnCloseButtonClicked() {
        if (!_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(false);
    }

    private void OnSettingsButtonClicked() {
        if (_isChangingScene || _settingsPanel.activeSelf) return;

        _settingsPanel.SetActive(true);
    }

    private void OnHideUIButtonClicked() {
        if (_isChangingScene) return;

        // UIの表示・非表示処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    private void OnNextSceneButtonClicked(int index) {
        if (_isChangingScene) return;

        // TODO 完全に実装したら消す
        if (_nextSceneNames[index] == "Audio") {
                if (_notYetInstalledPanel.activeSelf) return;
                _warningSentence.text = _warningTexts[1];
                _notYetInstalledPanel.SetActive(true);
                return;
        }

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, _nextSceneNames[index], false).Forget();
    }
    
    private void OnChargeButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // StartCoroutine(ChangeScene(1, "ModeSelection"));
        // TODO durationの変更
        GoNextSceneAsync(0, "ModeSelection", true).Forget();
    }

    // TODO GoNextSceneAsyncシリーズはこれを中心にオーバーライドさせること
    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時
        if (isShowLoadingPanel && _loadingPanel != null) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

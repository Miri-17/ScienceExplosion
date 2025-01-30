using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuController : AudioMixerController {
    #region Private Fields
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    // TODO 完全に実装したら消す.
    private List<string> _warningTexts = new List<string>() {
        "遊び方はまだ見られません。",
        "挑戦のリトライはまだできません。",
    };
    #endregion

    # region Serialized Fields
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _retryBattleButton = null;
    [SerializeField] private Button _retreatBattleButton = null;
    // TODO 完全に実装したら消す.
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private new void Start() {
        base.Start();

        Debug.Log("PauseMenuControllerのStart()が呼ばれた");

        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        _howToPlayButton.onClick.AddListener(() => OnHowToPlayButtonClicked());
        _retryBattleButton.onClick.AddListener(() => OnRetryBattleButtonClicked());
        _retreatBattleButton.onClick.AddListener(() => OnRetreatBattleButtonClicked());

        // TODO 完全に実装したら消す.
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => CloseNotYetInstalledPanel());

        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 押した瞬間に実行するようにする.
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => CloseNotYetInstalledPanel());
        //イベントの設定をEventTriggerに反映.
        _eventTrigger.triggers.Add(entry);
    }

    private void OnHowToPlayButtonClicked() {
        if (_isChangingScene) return;

        // 遊び方パネルを表示させる処理.

        // TODO 完全に実装したら消す.
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    private void OnRetryBattleButtonClicked() {
        if (_isChangingScene) return;

        // 再挑戦する処理.

        // TODO 完全に実装したら消す.
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[1];
        _notYetInstalledPanel.SetActive(true);
    }

    // TODO durationの変更, 逃げる演出の追加.
    private void OnRetreatBattleButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO 一時停止の方法をtimeScaleを使わないものに変更するかも.
        Time.timeScale = 1;
        GoNextSceneAsync(0, "Menu", false).Forget();
    }

    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること.

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時.
        if (isShowLoadingPanel && _loadingPanel != null) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時.
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // TODO 完全に実装したら消す.
    private void CloseNotYetInstalledPanel() {
        if (!_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SettingsController : AudioMixerController {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    #endregion

    # region
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private string _nextSceneName = "Credits";

    [SerializeField] private Image _settingsBg = null;
    [SerializeField] private Image _hologramBg = null;
    // SettingsのBackgroundをタップするとSettingsが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    [SerializeField] private GameObject _settingsPanel = null;
    [SerializeField] private Button _showCreditsButton = null;
    [SerializeField] private Button _deleteSaveDataButton = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    #endregion

    // TODO 合ってるか調べる
    private new void Start() {
        base.Start();

        Debug.Log("SettingsControllerのStart()が呼ばれた");

        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        _hologramBg.alphaHitTestMinimumThreshold = 1;
        _settingsBg.alphaHitTestMinimumThreshold = 1;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 押した瞬間に実行するようにする.
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => CloseSettings());
        //イベントの設定をEventTriggerに反映
        _eventTrigger.triggers.Add(entry);

        _showCreditsButton.onClick.AddListener(() => OnShowCreditsButtonClicked());
        _deleteSaveDataButton.onClick.AddListener(() => OnDeleteSaveDataButtonClicked());
        
        _settingsPanel.SetActive(false);

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
    }

    private void CloseSettings() {
        if (!_settingsPanel.activeSelf) return;

        Debug.Log("CloseSettings");
        _settingsPanel.SetActive(false);
    }

    private void OnShowCreditsButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, _nextSceneName, false).Forget();
    }

    private void OnDeleteSaveDataButtonClicked() {
        if (_isChangingScene) return;
        // セーブデータを消去する処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        _notYetInstalledPanel.SetActive(true);
    }

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

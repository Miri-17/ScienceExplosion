using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class ResultUIController : MonoBehaviour {
    private bool _isChangingScene = false;
    private CancellationToken _token = default;

    #region
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;

    [SerializeField] private TextMeshProUGUI _totalScore = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _backButton.onClick.AddListener(() => OnBackButtonClicked());

        _totalScore.text = GameDirector.Instance.Score.ToString("000000000");

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => CloseNotYetInstalledPanel());
        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 押した瞬間に実行するようにする.
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => CloseNotYetInstalledPanel());
        //イベントの設定をEventTriggerに反映
        _eventTrigger.triggers.Add(entry);
    }

    private void OnBackButtonClicked() {
        if (_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(true);
    }
    
    // TODO 完全に実装したら消す
    private void CloseNotYetInstalledPanel() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        // TODO durationの変更
        GoNextSceneAsync(0, "Title", true).Forget();
    }

    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        Debug.Log("Go to " + nextSceneName);
        
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
}

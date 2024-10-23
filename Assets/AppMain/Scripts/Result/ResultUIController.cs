using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ResultUIController : MonoBehaviour {
    #region
    private bool _isChangingScene = false;
    private CancellationToken _token = default;
    // TODO csvファイルを読み込めるように変更
    private List<string> _speeches = new List<string>() {
        "うふふ\n"
        + "私の薬、なかなか悪くないでしょ？",

        "よし！ これで実験素材は取り放題だ！",

        "ミンナ<sprite=3, color=#61009b>\n"
        + "ボク<sprite=3, color=#61009b>に拍手喝采かな<sprite=3, color=#61009b>",

        "おーほっほっ！\n"
        + "美しい土壌が広がったわね！",

        "フン、必然さ",

        "悲しい勝利だ......",

        "また一歩前進だな",

        "やった〜！ 大しょーり！",

        "良い成果だ！ 引き続き精進せよ",
    };
    private List<float> _speechBubblePosY = new List<float>() { 250, 200, 418, 418, 250, 200, 200, 418, 250 };
    #endregion

    #region
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;

    [SerializeField] private TextMeshProUGUI _totalScore = null;
    [SerializeField] private RectTransform _speechBubble = null;
    [SerializeField] private TextMeshProUGUI _speechText = null;

    [SerializeField] private List<Sprite> _rank = null;
    [SerializeField] private Image _rankImage = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _backButton.onClick.AddListener(() => OnBackButtonClicked());

        _totalScore.text = GameDirector.Instance.Score.ToString("000000000");
        
        _speechBubble.anchoredPosition = new Vector2(-593.0f, _speechBubblePosY[GameDirector.Instance.PlayerCharacterIndex]);
        _speechText.text = _speeches[GameDirector.Instance.PlayerCharacterIndex];

        switch (GameDirector.Instance.Rank) {
            case "C":
                _rankImage.sprite = _rank[0];
                break;
            case "B":
                _rankImage.sprite = _rank[1];
                break;
            case "A":
                _rankImage.sprite = _rank[2];
                break;
            case "S":
                _rankImage.sprite = _rank[3];
                break;
            case "SS":
                _rankImage.sprite = _rank[4];
                break;
            default:
                break;
        }

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

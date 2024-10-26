using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

public class ModeSelectionUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private RectTransform _trianglePlayAlone = null;
    private RectTransform _trianglePlayTwo = null;
    // TODO 完全に実装したら消す
    private List<string> _warningTexts = new List<string>() {
        "遊び方はまだ見られません。",
        "バトルモードではまだ遊べません。",
    };
    #endregion

    #region
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _backButton = null;

    [SerializeField] private TextMeshProUGUI _descriptionText = null;
    [SerializeField] private List<string> _descriptions = new List<string>() {
        "相手を倒すとストーリーが解放されるモードです。\n世界観を楽しみたい方に！",
        "お友達と通信して遊べるモードです。\n目指せ！ 世界最強のマッドサイエンティスト！",
    };
    [SerializeField] private string _nextSceneName = "PlayerSelection";
    [SerializeField] private Button _nextButton = null;
    [SerializeField] private RectTransform _hexagon = null;
    [Header("0...PlayAlone, 1...PlayTwo")]
    [SerializeField] private List<Image> _particles = null;
    [SerializeField] private List<Image> _triangles = null;
    [SerializeField] private List<Image> _explosions = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    public bool IsChangingScene { get => _isChangingScene; set => _isChangingScene = value; }
    // public GameObject NotYetInstalledPanel { get => _notYetInstalledPanel; set => _notYetInstalledPanel = value; }

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        #region // ボタン設定
        ChangeAlphaHitThreshold(_howToPlayButton, 1.0f);
        _howToPlayButton.onClick.AddListener(() => OnHowToPlayButtonClicked());
        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());

        ChangeAlphaHitThreshold(_nextButton, 1.0f);
        _nextButton.onClick.AddListener(() => OnNextButtonClicked());
        #endregion

        _hexagon.DOScale(2f, 1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(_hexagon.gameObject);
        var hexagonImage = _hexagon.GetComponent<Image>();
        hexagonImage.DOFade(0, 1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(_hexagon.gameObject);

        _trianglePlayAlone = _triangles[0].GetComponent<RectTransform>();
        _trianglePlayTwo = _triangles[1].GetComponent<RectTransform>();
        _trianglePlayAlone.DOAnchorPos(new Vector2(368.0f, -434.0f), 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_trianglePlayAlone.gameObject);
        _trianglePlayTwo.DOAnchorPos(new Vector2(-368.0f, -434.0f), 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_trianglePlayTwo.gameObject);

        // TODO ここが良くないので直すこと
        // _playAloneButton.interactable = false;
        _particles[0].enabled = true;
        _particles[1].enabled = false;
        _triangles[0].enabled = true;
        _triangles[1].enabled = false;
        _explosions[0].enabled = false;
        _explosions[1].enabled = false;

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

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    private void OnHowToPlayButtonClicked() {
        if (_isChangingScene) return;

        // 遊び方パネルを表示させる処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    // TODO 完全に実装したら消す
    private void CloseNotYetInstalledPanel() {
        if (!_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(false);
    }

    private void OnBackButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, "Menu", false).Forget();
    }

    public void OnPlayAloneButtonClicked() {
        _nextSceneName = "PlayerSelection";
        _descriptionText.text = _descriptions[0];
        _particles[1].enabled = false;
        _particles[0].enabled = true;
        _triangles[1].enabled = false;
        _triangles[0].enabled = true;
    }

    public void OnPlayTwoButtonClicked() {
        // TODO シーン名決まったら変更
        _nextSceneName = "Network";
        _descriptionText.text = _descriptions[1];
        _particles[0].enabled = false;
        _particles[1].enabled = true;
        _triangles[0].enabled = false;
        _triangles[1].enabled = true;
    }

    private void OnNextButtonClicked() {
        if (_isChangingScene) return;

        // TODO 完全に実装したら消す
        if (_nextSceneName != "PlayerSelection") {
            if (_notYetInstalledPanel.activeSelf) return;
            _warningSentence.text = _warningTexts[1];
            _notYetInstalledPanel.SetActive(true);
            return;
        }

        _explosions[0].enabled = true;
        // メニューで選んでいるキャラをプレイヤーセレクトで選ばれている初期キャラクターにする
        GameDirector.Instance.PlayerCharacterIndex = GameDirector.Instance.SelectedCharacterIndex;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, _nextSceneName, false).Forget();
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

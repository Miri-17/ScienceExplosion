using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class EnemySelectionUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private readonly float SCALE_DURATION = 0.5f;

    // TODO 完全に実装したら消す
    private List<string> _warningTexts = new List<string>() {
        "遊び方はまだ見られません。",
        "クラックス・クルーを選ばないと、次のシーンに進めません。",
    };
    #endregion

    #region
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _chargeButton = null;
    // [SerializeField] private Image _unmask = null;
    [SerializeField] private RectTransform _unmask = null;
    [SerializeField] private Image _screen = null;

    [SerializeField] private List<Button> _placeButtons = null;
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private Image _icon = null;
    [SerializeField] private Image _place = null;

    /// TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        ChangeAlphaHitThreshold(_howToPlayButton, 1.0f);
        ChangeAlphaHitThreshold(_backButton, 1.0f);
        ChangeAlphaHitThreshold(_chargeButton, 1.0f);
        _howToPlayButton.onClick.AddListener(() => OnHowToPlayButtonClicked());
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        _chargeButton.onClick.AddListener(() => OnChargeButtonClicked());

        _screen.enabled = false;

        for (var i = 0; i < _placeButtons.Count; i++) {
            var index = i;
            _placeButtons[i].onClick.AddListener(() => OnPlaceButtonClicked(index));
        }

        UpdateUI(GameDirector.Instance.EnemyCharacterIndex);

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
        GoNextSceneAsync(0, "PlayerSelection", false).Forget();
    }

    private void OnPlaceButtonClicked(int index) {
        GameDirector.Instance.EnemyCharacterIndex = index;

        UpdateUI(index);
    }

    private void UpdateUI(int index) {
        var character = _charactersDB.GetCharacter(index);

        _place.sprite = character.PlaceNameSprite;
        _icon.sprite = character.IconSprite;
    }

    private void OnChargeButtonClicked() {
        if (_isChangingScene) return;

        // TODO 完全に実装したら消す
        if (GameDirector.Instance.EnemyCharacterIndex != 2) {
            if (_notYetInstalledPanel.activeSelf) return;
            _warningSentence.text = _warningTexts[1];
            _notYetInstalledPanel.SetActive(true);
            return;
        }

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);

        GameDirector.Instance.Score = 0;

        IrisOut();
        // TODO durationの変更
        GoNextSceneAsync(0.5f, "Battle", false).Forget();
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

    private void IrisOut() {
        _screen.enabled = true;
        _unmask.DOScale(new Vector3(0, 0, 0), SCALE_DURATION)
            .SetEase(Ease.InBack)
            .SetLink(_unmask.gameObject);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class CutscenesUIController : MonoBehaviour {
    #region Private Fields
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    
    private int _previousCutsceneIndex = 0;
    private List<SetStill> _setStills = null;

    // TODO 完全に実装したら消す.
    private List<string> _warningTexts = new List<string>() {
        "スチルの拡大機能はまだ実装されていません。",
        "ストーリーはまだ見られません。",
    };
    #endregion

    #region Serialized Fields
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _expandButton = null;
    [SerializeField] private Button _readButton = null;
    [SerializeField] private string _previousSceneName = "";

    [SerializeField] private CutscenesDB _cutscenesDB = null;
    [SerializeField] private Image _title = null;
    [SerializeField] private List<Image> _icons = new List<Image>();
    [SerializeField] private List<Button> _stillButtons = new List<Button>();

    // TODO 完全に実装したら消す.
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        ChangeAlphaHitThreshold(_expandButton, 1.0f);
        _expandButton.onClick.AddListener(() => OnExpandButtonClicked());
        ChangeAlphaHitThreshold(_readButton, 1.0f);
        _readButton.onClick.AddListener(() => OnReadButtonClicked());

        _setStills = new List<SetStill>(_stillButtons.Count);
        for (var i = 0; i < _stillButtons.Count; i++) {
            var index = i;
            _stillButtons[i].onClick.AddListener(() => OnStillButtonClicked(index));
            // FIXME これ、リストへの追加の仕方おかしいかも.
            _setStills.Add(_stillButtons[i].GetComponent<SetStill>());
        }
        ChangeCutscene(0);

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

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    private void OnBackButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更.
        GoNextSceneAsync(0, _previousSceneName, false).Forget();
    }

    private void OnExpandButtonClicked() {
        if (_isChangingScene) return;

        // 2人プレイに行く処理.

        // TODO 完全に実装したら消す.
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }
    
    private void OnReadButtonClicked() {
        if (_isChangingScene) return;

        // 2人プレイに行く処理.

        // TODO 完全に実装したら消す.
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[1];
        _notYetInstalledPanel.SetActive(true);
    }

    // TODO 完全に実装したら消す.
    private void CloseNotYetInstalledPanel() {
        if (!_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(false);
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

    private void ChangeCutscene(int index) {
        // Debug.Log(_cutsceneIndex);
        _setStills[index].SetSelection(true);
        _previousCutsceneIndex = index;

        var cutscene = _cutscenesDB.GetCutscene(index);
        _title.sprite = cutscene.TitleSprite;

        _icons[0].sprite = cutscene.RelatedCharacterSprites[0];
        // if (cutscene.RelatedCharacterSprites[1] != null)
        if (cutscene.RelatedCharacterSprites.Count > 1)
            _icons[1].sprite = cutscene.RelatedCharacterSprites[1];
        else
            _icons[1].sprite = null;
    }

    private void OnStillButtonClicked(int index) {
        Debug.Log("index: " + index);
        Debug.Log("previousIndex" + _previousCutsceneIndex);
        _setStills[_previousCutsceneIndex].SetSelection(false);

        ChangeCutscene(index);
    }
}

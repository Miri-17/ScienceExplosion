using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerSelectionUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private List<CharacterButton> _characterButtonScripts = null;
    private int _previousPlayerIndex = 0;
    #endregion

    #region
    [SerializeField] private PlayerSelectionController _playerSelectionController = null;
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private Button _selectionButton = null;

    [SerializeField] private PlayerSelectionDB _playerSelectionDB = null;
    [SerializeField] private Image _nameImage = null;
    [SerializeField] private Image _majorImage = null;
    [SerializeField] private Image _typeImage = null;
    [SerializeField] private TextMeshProUGUI _descriptionText = null;
    // TODO ここ、あまり良いコードではないので直すこと
    [Header("0...HP, 1...Damage")]
    [SerializeField] private List<TextMeshProUGUI> _maxTexts = null;
    [Header("0...HP, 1...Damage")]
    [SerializeField] private List<Image> _testTubeFill = null;
    [Header("0...Unique Puzzle, 1...Ex Puzzle")]
    [SerializeField] private List<TextMeshProUGUI> _maxNumberTexts = null;
    [SerializeField] private List<Image> _uniquePuzzleScales = null;
    [SerializeField] private Sprite _uniquePuzzleScaleDefault = null;
    [SerializeField] private List<Image> _exPuzzleScales = null;
    [SerializeField] private Sprite _exPuzzleScaleDefault = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        #region // ボタンの設定
        ChangeAlphaHitThreshold(_howToPlayButton, 1.0f);
        _howToPlayButton.onClick.AddListener(() => OnHowToPlayButtonClicked());
        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());

        _characterButtonScripts = new List<CharacterButton>(_characterButtons.Count);
        for (var i = 0; i < _characterButtons.Count; i++) {
            var index = i;
            ChangeAlphaHitThreshold(_characterButtons[i], 1.0f);
            _characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
            // FIXME これ、リストへの追加の仕方おかしいかも
            _characterButtonScripts.Add(_characterButtons[i].GetComponent<CharacterButton>());
        }

        ChangeAlphaHitThreshold(_selectionButton, 1.0f);
        _selectionButton.onClick.AddListener(() => OnSelectionButtonClicked());
        #endregion

        _previousPlayerIndex = GameDirector.Instance.PlayerCharacterIndex;
        _characterButtonScripts[_previousPlayerIndex].SetSelection(true);

        UpdateUI(_previousPlayerIndex);

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
        GoNextSceneAsync(0, "ModeSelection", false).Forget();
    }

    private void OnCharacterButtonClicked(int index) {
        _characterButtonScripts[_previousPlayerIndex].SetSelection(false);

        _playerSelectionController.UpdatePlayerCharacter(index);

        UpdateUI(index);

        _characterButtonScripts[index].SetSelection(true);
        _previousPlayerIndex = index;
    }

    private void UpdateUI(int index) {
        _nameImage.sprite = _playerSelectionController.Character.NameSprite;
        _majorImage.sprite = _playerSelectionController.Character.MajorSprite;
        _typeImage.sprite = _playerSelectionDB.TypeSprites[index];
        _descriptionText.text = _playerSelectionDB.Descriptions[index];

        var hp = _playerSelectionDB.HPs[index];
        // TODO 元からenum使って対応づけとくと綺麗かも
        switch (hp) {
            case 1:
                _maxTexts[0].text = "最小";
                break;
            case 2:
                _maxTexts[0].text = "小";
                break;
            case 3:
                _maxTexts[0].text = "中";
                break;
            case 4:
                _maxTexts[0].text = "大";
                break;
            default:
                _maxTexts[0].text = "最大";
                break;
        }
        _testTubeFill[0].sprite = _playerSelectionDB.TestTubeSprites[hp - 1];
        _testTubeFill[0].color = _playerSelectionController.Character.UniqueColor;

        var damage = _playerSelectionDB.Damages[index];
        // TODO 元からenum使って対応づけとくと綺麗かも
        switch (damage) {
            case 1:
                _maxTexts[1].text = "最小";
                break;
            case 2:
                _maxTexts[1].text = "小";
                break;
            case 3:
                _maxTexts[1].text = "中";
                break;
            case 4:
                _maxTexts[1].text = "大";
                break;
            default:
                _maxTexts[1].text = "最大";
                break;
        }
        _testTubeFill[1].sprite = _playerSelectionDB.TestTubeSprites[damage - 1];
        _testTubeFill[1].color = _playerSelectionController.Character.UniqueColor;

        var requiredNumberPuzzle = _playerSelectionDB.RequiredNumberPuzzle[index];
        _maxNumberTexts[0].text = requiredNumberPuzzle.ToString();
        // TODO コレクションとラムダ式使えばもっと綺麗に書ける気がする
        for (int i = 0; i < _uniquePuzzleScales.Count; i++) {
            if (i + 1 > requiredNumberPuzzle) {
                _uniquePuzzleScales[i].sprite = _uniquePuzzleScaleDefault;
                continue;
            }
            _uniquePuzzleScales[i].sprite = _playerSelectionDB.PuzzleSprites[index];
        }

        var requiredNumberEx = _playerSelectionDB.RequiredNumberEx[index];
        _maxNumberTexts[1].text = requiredNumberEx.ToString();
        // TODO コレクションとラムダ式使えばもっと綺麗に書ける気がする
        for (int i = 0; i < _exPuzzleScales.Count; i++) {
            if (i + 1 > requiredNumberEx) {
                _exPuzzleScales[i].sprite = _exPuzzleScaleDefault;
                continue;
            }
            _exPuzzleScales[i].sprite = _playerSelectionDB.ExSprites[index];
        }
    }

    private void OnSelectionButtonClicked() {
        if (_isChangingScene) return;

        // TODO マイラ・キメラに変更
        GameDirector.Instance.EnemyCharacterIndex = 2;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, "EnemySelection", false).Forget();
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

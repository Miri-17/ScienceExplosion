using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CharactersUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private List<CharacterButton> _characterButtonScripts = null;
    private int _previousCharacterIndex = 0;
    private int _informatioinIndex = 0;
    #endregion

    #region
    [SerializeField] private CharactersController _charactersController = null;
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private Button _zoomButton = null;
    [SerializeField] private Button _callButton = null;

    [Header("0...未設定, 1...設定済み")]
    [SerializeField] private Image _callTextImage = null;
    [SerializeField] private List<Sprite> _callTextSprites = null;
     
    [SerializeField] private Image _nameImage = null;
    [SerializeField] private Image _majorImage = null;

    [SerializeField] private CharacterSelectionDB _characterSelectionDB = null;
    [SerializeField] private TextMeshProUGUI _characterDescriptionText = null;
    
    [Header("Government")]
    [SerializeField] private TextMeshProUGUI _governmentNameText = null;
    [SerializeField] private Image _governmentName = null;
    [SerializeField] private TextMeshProUGUI _governmentDescription = null;
    
    [Header("Lab")]
    [SerializeField] private TextMeshProUGUI _labNameText = null;
    [SerializeField] private Image _labName = null;
    [SerializeField] private TextMeshProUGUI _labDescription = null;

    [Header("UniquePuzzle")]
    [SerializeField] private Image _uniquePuzzleName = null;
    [SerializeField] private TextMeshProUGUI _uniquePuzzleDescription = null;

    [Header("Explosion")]
    [SerializeField] private Image _explosionName = null;
    [SerializeField] private TextMeshProUGUI _explosionDescription = null;
    
    [SerializeField] private RectTransform _triangleLeft = null;
    [SerializeField] private RectTransform _triangleRight = null;
    [SerializeField] private Button _triangleLeftButton = null;
    [SerializeField] private Button _triangleRightButton = null;
    [Header("0...Lab, 1...Skill, 2...Level")]
    [SerializeField] private List<GameObject> _informationPanels = null;


    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    // NotYetInstalledPanelのBackgroundをタップするとPanelが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        // _triangleLeft.DOAnchorPosX(-420.0f, 0.8f)
        _triangleLeft.DOAnchorPosX(-432.0f, 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_triangleLeft.gameObject);

        // _triangleRight.DOAnchorPosX(452.0f, 0.8f)
        _triangleRight.DOAnchorPosX(464.0f, 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_triangleRight.gameObject);

        #region // ボタンの設定
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

        ChangeAlphaHitThreshold(_zoomButton, 1.0f);
        _zoomButton.onClick.AddListener(() => OnZoomButtonClicked());
        ChangeAlphaHitThreshold(_callButton, 1.0f);
        _callButton.onClick.AddListener(() => OnCallButtonClicked());

        _triangleLeftButton.onClick.AddListener(() => OnTriangleButtonClicked(-1));
        _triangleRightButton.onClick.AddListener(() => OnTriangleButtonClicked(1));
        // TODO informationPanelsの初期化処理 (LINQ?)
        // _informationPanels[_informatioinIndex].SetActive(true);
        // _informationPanels[_informatioinIndex].SetActive(false);
        // _informationPanels[_informatioinIndex].SetActive(false);
        #endregion

        _previousCharacterIndex = GameDirector.Instance.SelectedCharacterIndex;
        _characterButtonScripts[_previousCharacterIndex].SetSelection(true);

        UpdateUI(_previousCharacterIndex);

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

    private void OnCharacterButtonClicked(int index) {
        _characterButtonScripts[_previousCharacterIndex].SetSelection(false);

        _charactersController.UpdatePlayerCharacter(index);

        UpdateUI(index);

        _characterButtonScripts[index].SetSelection(true);
        _previousCharacterIndex = index;
    }

    private void UpdateUI(int index) {
        _nameImage.sprite = _charactersController.Character.NameSprite;
        _majorImage.sprite = _charactersController.Character.MajorSprite;

        _characterDescriptionText.text = _characterSelectionDB.CharacterDescriptions[index];

        _governmentNameText.text = _characterSelectionDB.GovernmentNameTexts[index];
        _governmentName.sprite = _characterSelectionDB.GovernmentNameImages[index];
        _governmentDescription.text = _characterSelectionDB.GovernmentDescriptions[index];

        _labNameText.text = _characterSelectionDB.LabNameTexts[index];
        _labName.sprite = _characterSelectionDB.LabNameImages[index];
        _labDescription.text = _characterSelectionDB.LabDescriptions[index];

        _uniquePuzzleName.sprite = _characterSelectionDB.UniquePuzzleNames[index];
        _uniquePuzzleDescription.text = _characterSelectionDB.UniquePuzzleDescriptions[index];

        _explosionName.sprite = _characterSelectionDB.ExplosionNames[index];
        _explosionDescription.text = _characterSelectionDB.ExplosionDescriptions[index];

        if (index == GameDirector.Instance.SelectedCharacterIndex) {
            _callButton.interactable = false;
            _callTextImage.sprite = _callTextSprites[1];
        } else {
            _callButton.interactable = true;
            _callTextImage.sprite = _callTextSprites[0];
        }
    }

    private void OnTriangleButtonClicked(int number) {
        Debug.Log(number);
        _informationPanels[_informatioinIndex].SetActive(false);

        _informatioinIndex += number;
        if (_informatioinIndex < 0)
            _informatioinIndex = _informationPanels.Count - 1;
        else if (_informatioinIndex >= _informationPanels.Count)
            _informatioinIndex = 0;
        _informationPanels[_informatioinIndex].SetActive(true);
    }

    private void OnZoomButtonClicked() {
        if (_isChangingScene) return;

        // UIを非表示にする処理
        // キャラクターを大きくする処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        // _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    private void OnCallButtonClicked() {
        if (_isChangingScene) return;

        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);

        GameDirector.Instance.SelectedCharacterIndex = _previousCharacterIndex;

        _callButton.interactable = false;
        _callTextImage.sprite = _callTextSprites[1];
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

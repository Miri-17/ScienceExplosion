using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class PlayerSelectionUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private List<CharacterButton> _characterButtonScripts = null;
    private int _previousPlayerIndex = 0;
    // TODO 完全に実装したら消す
    private List<string> _warningTexts = new List<string>() {
        "遊び方はまだ見られません。",
        "ロジーちゃんを選ばないと、次のシーンに進めません。",
    };
    #endregion

    #region
    [SerializeField] private PlayerSelectionController _playerSelectionController = null;
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private Button _selectionButton = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    [SerializeField] private Button _closeButton = null;
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
            _characterButtonScripts.Add(_characterButtons[i].GetComponent<CharacterButton>());
        }

        ChangeAlphaHitThreshold(_selectionButton, 1.0f);
        _selectionButton.onClick.AddListener(() => OnSelectionButtonClicked());
        #endregion

        _previousPlayerIndex = GameDirector.Instance.PlayerCharacterIndex;
        _characterButtonScripts[_previousPlayerIndex].SetSelection(true);


        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked());
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
    private void OnCloseButtonClicked() {
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
        _characterButtonScripts[index].SetSelection(true);
        _previousPlayerIndex = index;
    }

    private void OnSelectionButtonClicked() {
        if (_isChangingScene) return;

        // TODO 完全に実装したら消す
        if (GameDirector.Instance.PlayerCharacterIndex != 7) {
            if (_notYetInstalledPanel.activeSelf) return;
            _warningSentence.text = _warningTexts[1];
            _notYetInstalledPanel.SetActive(true);
            return;
        }

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

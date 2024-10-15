using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class CharactersUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private List<CharacterButton> _characterButtonScripts = null;
    private int _previousCharacterIndex = 0;
    #endregion

    #region
    [SerializeField] private CharactersController _charactersController = null;
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private Button _zoomButton = null;
    [SerializeField] private Button _callButton = null;
    
    [SerializeField] private Image _nameImage = null;
    [SerializeField] private Image _majorImage = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

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
        #endregion

        _previousCharacterIndex = GameDirector.Instance.PlayerCharacterIndex;
        _characterButtonScripts[_previousCharacterIndex].SetSelection(true);

        UpdateUI(_previousCharacterIndex);

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked());
    }

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
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
        GameDirector.Instance.SelectedCharacterIndex = _previousCharacterIndex;
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

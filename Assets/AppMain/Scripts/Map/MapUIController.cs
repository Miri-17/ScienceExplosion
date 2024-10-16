using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using TMPro;

public class MapUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;

    private int _currentPlaceIndex = 0;
    #endregion

    #region
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _goButton = null;
    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;

    [SerializeField] private List<Button> _placeButtons = null;
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private Image _icon = null;
    [SerializeField] private Image _place = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        ChangeAlphaHitThreshold(_goButton, 1.0f);
        _goButton.onClick.AddListener(() => OnGoButtonClicked());

        for (var i = 0; i < _placeButtons.Count; i++) {
            var index = i;
            _placeButtons[i].onClick.AddListener(() => OnPlaceButtonClicked(index));
        }

        _currentPlaceIndex = GameDirector.Instance.SelectedPlaceIndex;
        UpdateUI(_currentPlaceIndex);

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

    private void OnPlaceButtonClicked(int index) {
        // GameDirector.Instance.EnemyCharacterIndex = index;
        _currentPlaceIndex = index;

        UpdateUI(_currentPlaceIndex);
    }

    private void UpdateUI(int index) {
        var character = _charactersDB.GetCharacter(index);

        _place.sprite = character.PlaceNameSprite;
        _icon.sprite = character.IconSprite;
    }

    private void OnGoButtonClicked() {
        if (_isChangingScene) return;

        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        
        GameDirector.Instance.SelectedPlaceIndex = _currentPlaceIndex;

        // TODO 完全に実装したら消す
        // if (_notYetInstalledPanel.activeSelf) return;
        // _notYetInstalledPanel.SetActive(true);
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

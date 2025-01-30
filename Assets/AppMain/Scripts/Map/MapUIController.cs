using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

public class MapUIController : MonoBehaviour {
    #region Private Fields
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;

    private int _currentPlaceIndex = 0;
    #endregion

    #region Serialized Fields
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _goButton = null;

    [SerializeField] private List<Button> _placeButtons = null;
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private Image _icon = null;
    [SerializeField] private Image _place = null;

    [SerializeField] private Image _goTextImage = null;
    [Header("0...行く！, 1...設定済み")]
    [SerializeField] private List<Sprite> _goTextSprites = null;
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
        GoNextSceneAsync(0, "Menu", false).Forget();
    }

    private void OnPlaceButtonClicked(int index) {
        _currentPlaceIndex = index;

        UpdateUI(_currentPlaceIndex);
    }

    private void UpdateUI(int index) {
        var character = _charactersDB.GetCharacter(index);

        _place.sprite = character.PlaceNameSprite;
        _icon.sprite = character.IconSprite;

        if (index == GameDirector.Instance.SelectedPlaceIndex) {
            _goButton.interactable = false;
            _goTextImage.sprite = _goTextSprites[1];
        } else {
            _goButton.interactable = true;
            _goTextImage.sprite = _goTextSprites[0];
        }
    }

    private void OnGoButtonClicked() {
        if (_isChangingScene) return;

        _audioClip_SE = SE.Instance.audioClips[1];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        
        GameDirector.Instance.SelectedPlaceIndex = _currentPlaceIndex;

        _goButton.interactable = false;
        _goTextImage.sprite = _goTextSprites[1];
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
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

public class CreditsUIController : MonoBehaviour {
    #region Private Fields
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    #endregion

    #region Serialized Fields
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;

    [SerializeField] private string _nextSceneName = "Menu";

    [SerializeField] private RectTransform _scrollingObjects = null;
    [SerializeField] private float _endAnchorPosY = 4335.0f;
    [SerializeField] private float _time = 60.0f;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        #region // ボタン設定
        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        #endregion
        
        _scrollingObjects.DOAnchorPosY(_endAnchorPosY, _time)
            .SetEase(Ease.Linear)
            .OnComplete(CreditsEnded)
            .SetLink(_scrollingObjects.gameObject);
    }

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    private void CreditsEnded() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        Time.timeScale = 1;
        // TODO durationの変更.
        GoNextSceneAsync(0, _nextSceneName, false).Forget();
    }

    private void OnBackButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        Time.timeScale = 1;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更.
        GoNextSceneAsync(0, _nextSceneName, false).Forget();
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

using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

public class ResultUIController : MonoBehaviour {
    private bool _isChangingScene = false;
    private CancellationToken _token = default;

    [SerializeField] private GameObject _warningPanel = null;
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private Button _closeButton = null;

    private void Start() {
        _warningPanel.SetActive(false);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked());
    }

    private void OnBackButtonClicked() {
        if (_warningPanel.activeSelf) return;

        _warningPanel.SetActive(true);
    }
    
    private void OnCloseButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        // TODO durationの変更
        GoNextSceneAsync(0, "Title", true).Forget();
    }

    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時
        if (isShowLoadingPanel) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

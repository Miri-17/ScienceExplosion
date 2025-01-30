using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour {
    private bool _isChangingScene = false;
    private CancellationToken _token = default;

    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private BattleUIController _battleUIController = null;
    
    private void Update() {
        if (_battleUIController.IsTimeUp) {
            if (_isChangingScene) return;

            _isChangingScene = true;

            GameDirector.Instance.Score = _battleUIController.CurrentScore;
            GameDirector.Instance.Rank = _battleUIController.CurrentRank;
            
            // TODO durationの変更.
            GoNextSceneAsync(0, "Result", false).Forget();
        }
    }

    // TODO GoNextSceneAsyncシリーズはこれを中心にオーバーライドさせること.
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

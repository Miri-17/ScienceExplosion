using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    #region
    private bool _isTitleCutEnded = false;
    private bool _isChangingScene = false;
    private CancellationToken _token;
    #endregion

    #region
    [SerializeField] private ImageFade _imageFade = null;
    [SerializeField] private Image _tapToStart = null;
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private string _nextSceneName = "Menu";
    #endregion

    private void Start() {
        _tapToStart.enabled = false;
        _tapToStart.DOFade(0.0f, 1.0f)
            .SetEase(Ease.InQuart)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_tapToStart.gameObject);
        
        _imageFade.enabled = false;

        _playableDirector.Play();
    }

    private void Update() {
        if (_isTitleCutEnded) {
            if (!_isChangingScene && Input.GetMouseButtonDown(0)) {
                _isChangingScene = true;
                _token = this.GetCancellationTokenOnDestroy();
                GoNextSceneAsync(_imageFade.FadeDuration).Forget();
            }
        } else {
            // タップで演出をスキップ
            if (Input.GetMouseButtonDown(0)) {
                _isTitleCutEnded = true;
                _playableDirector.time = _playableDirector.duration;
                _tapToStart.enabled = true;
            } else if (_playableDirector.time >= _playableDirector.duration) {
                _isTitleCutEnded = true;
                _tapToStart.enabled = true;
            }
        }
    }

    private async UniTaskVoid GoNextSceneAsync(float duration) {
        _imageFade.enabled = true;
        
        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + _nextSceneName);
        SceneManager.LoadScene(_nextSceneName);
    }
}

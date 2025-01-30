using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using System.Collections.Generic;

public class TitleController : MonoBehaviour {
    #region Private Fields
    private bool _isTitleCutEnded = false;
    private bool _isChangingScene = false;
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    #endregion

    #region Serialized Fields
    [SerializeField] private List<TitleL2D> _titleL2Ds = new List<TitleL2D>();
    [SerializeField] private ImageFade _imageFade = null;
    [SerializeField] private Image _tapToStart = null;
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private string _nextSceneName = "Menu";
    [SerializeField] private GameObject _loadingPanel = null;
    #endregion

    private void Start() {
        _tapToStart.enabled = false;
        _tapToStart.DOFade(0.0f, 1.0f)
            .SetEase(Ease.InQuart)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_tapToStart.gameObject);
        
        _imageFade.enabled = false;

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();
        _audioClip_SE = SE.Instance.audioClips[1];

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
            // タップで演出をスキップ.
            if (Input.GetMouseButtonDown(0)) {
                _isTitleCutEnded = true;
                _playableDirector.time = _playableDirector.duration;
                _tapToStart.enabled = true;
                foreach (var titleL2D in _titleL2Ds)
                    titleL2D.PlayAnimation();
            } else if (_playableDirector.time >= _playableDirector.duration) {
                _isTitleCutEnded = true;
                _tapToStart.enabled = true;
                foreach (var titleL2D in _titleL2Ds)
                    titleL2D.PlayAnimation();
            }
        }
    }

    private async UniTaskVoid GoNextSceneAsync(float duration) {
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        _imageFade.enabled = true;
        
        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + _nextSceneName);
        
        // ローディングパネルがある時.
        _loadingPanel.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(_nextSceneName);
        await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時.
        // SceneManager.LoadScene(_nextSceneName);
    }
}

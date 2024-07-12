using UnityEngine;
using DG.Tweening;
using System.Threading;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

[RequireComponent(typeof(CanvasGroup))]
public class UITransition : MonoBehaviour {
    // RectTrasform保管用
    private RectTransform _rectTransform = null;
    // RectTransform取得用
    public RectTransform RectTransform {
        get {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    // キャンバスグループ保管用
    private CanvasGroup _canvasGroup = null;
    // キャンバスグループ取得用
    public CanvasGroup CanvasGroup {
        get {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
            return _canvasGroup;
        }
    }

    [System.Serializable]
    public class TransitionParams {
        public bool IsActive = true;
        public Vector2 In = new Vector2(0, 1.0f);
        public Vector2 Out = new Vector2(1.0f, 0);
    }

    [SerializeField] private TransitionParams _fadeTransitionParams = new TransitionParams();
    [SerializeField] private TransitionParams _positionTransitionParams = new TransitionParams(){
        IsActive = false,
        In = Vector2.zero,
        Out = Vector2.zero
    };
    [SerializeField] private float _duration = 1.0f;

    // TODO メソッド内以外で使わないならメソッド内に移動
    private Sequence _inSequence = null;
    private Sequence _outSequence = null;
    //

    private CancellationTokenSource _inCancellation = null;
    private CancellationTokenSource _outCancellation = null;
    
    /// <summary>
    /// あるビューに切り替わる時の処理をする
    /// </summary>
    /// <param name="onCompleted"></param>
    public void TransitionIn(UnityAction onCompleted = null) {
        if (_inSequence != null) {
            _inSequence.Kill();
            _inSequence = null;
        }
        _inSequence = DOTween.Sequence();

        if (_fadeTransitionParams.IsActive && CanvasGroup != null) {
            CanvasGroup.alpha = _fadeTransitionParams.In.x;

            _inSequence.Join(
                CanvasGroup.DOFade(_fadeTransitionParams.In.y, _duration)
                    .SetLink(this.gameObject)
            );
        }

        if (_positionTransitionParams.IsActive) {
            var current = RectTransform.transform.localPosition;
            RectTransform.transform.localPosition = new Vector2(_positionTransitionParams.In.x, _positionTransitionParams.In.y);
            _inSequence.Join(
                RectTransform.DOAnchorPos(current, _duration)
                    .SetLink(this.gameObject)
            );
        }

        _inSequence
            .SetLink(this.gameObject)
            // onCompletedがヌルでなければ、格納されているUnityAction(ここではisDone = true)を実行
            .OnComplete(() => onCompleted?.Invoke());
    }
    
    // あるビューから切り替わる時の処理をする
    private void TransitionOut(UnityAction onCompleted = null) {
        if (_outSequence != null) {
            _outSequence.Kill();
            _outSequence = null;
        }
        _outSequence = DOTween.Sequence();

        if (_fadeTransitionParams.IsActive && CanvasGroup != null) {
            CanvasGroup.alpha = _fadeTransitionParams.Out.x;

            _outSequence.Join(
                CanvasGroup.DOFade(_fadeTransitionParams.Out.y, _duration)
                    .SetLink(this.gameObject)
            );
        }

        _outSequence
            .SetLink(this.gameObject)
            .OnComplete(() => onCompleted?.Invoke());
    }

    /// <summary>
    /// 遷移時のフェードイン処理の終了待機をする
    /// </summary>
    /// <returns></returns>
    public async UniTask TransitionInWait() {
        var isDone = false;

        if (_inCancellation != null)
            _inCancellation.Cancel();
        _inCancellation = new CancellationTokenSource();

        TransitionIn(() => { isDone = true; });

        try {
            await UniTask.WaitUntil(() => isDone == true, PlayerLoopTiming.Update, _inCancellation.Token);
        } catch (System.OperationCanceledException e) {
            Debug.Log("Task was canceled: " + e);
        }
    }
    
    /// <summary>
    /// 遷移時のフェードアウト処理の終了待機をする
    /// </summary>
    /// <returns></returns>
    public async UniTask TransitionOutWait() {
        var isDone = false;

        if (_outCancellation != null)
            _outCancellation.Cancel();
        _outCancellation = new CancellationTokenSource();

        TransitionOut(() => { isDone = true; });

        try {
            await UniTask.WaitUntil(() => isDone == true, PlayerLoopTiming.Update, _outCancellation.Token);
        } catch (System.OperationCanceledException e) {
            Debug.Log("Task was canceled: " + e);
        }
    }
    
    // 破棄された時のコールバック
    private void OnDestroy() {
        if (_inCancellation != null)
            _inCancellation.Cancel();
        if (_outCancellation != null)
            _outCancellation.Cancel();
    }
}

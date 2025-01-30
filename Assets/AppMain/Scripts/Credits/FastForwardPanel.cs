using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FastForwardPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    private float _time = 0;
    private bool _isDown = false;

    #region Serialized Fields
    [SerializeField, Header("何秒で長押し判定になるか")] private float _longTapTime = 1.0f;
    [SerializeField, Header("何倍速になるか")] private float _playSpeed = 4.0f;
    [SerializeField] private Image _quadSpeed = null;
    #endregion

    private void Start() {
        // TODO Time.timeScaleを使うことによって、下のDoTweenや、背景のマテリアルの速さまでおかしくなってしまうので
        // それを使わないように変更すること.
        _quadSpeed.enabled = false;
        // _quadSpeed.DOFade(0.0f, 1.0f)
        //     .SetEase(Ease.InQuart)
        //     .SetLoops(-1, LoopType.Yoyo)
        //     .SetLink(_quadSpeed.gameObject);
    }

    private void Update() {
        if (!_isDown) return;

        _time += Time.deltaTime;
        if (_time >= _longTapTime) {
            _isDown = false;
            Debug.Log("2倍速にする処理を呼ぶ");
            Time.timeScale = _playSpeed;
            _quadSpeed.enabled = true;
        }
    }

    /// <summary>
    /// クレジット画面に手を置いた時の処理.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) {
        _isDown = true;
        _time = 0;
    }

    /// <summary>
    /// クレジット画面から手を離した時の処理.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) {
        _isDown = false;
        Debug.Log("1倍速に戻す処理を呼ぶ");
        Time.timeScale = 1;
        _quadSpeed.enabled = false;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {
    #region Private Fields
    private Image _fadeImage = null;
    private bool _isCompleted = false;
    private float _red;
    private float _green;
    private float _blue;
    private float _currentAlpha;
    private float _fadeSpeed;
    #endregion

    [Header("フェードにかかる時間")]
    [SerializeField] private float _fadeDuration = 1.0f;
    [Header("フェードインかフェードアウトか")]
    [SerializeField] private bool _isFadeIn = false;

    public float FadeDuration { get => _fadeDuration; set => _fadeDuration = value;}

    private void Start() {
        _fadeImage = GetComponent<Image>();
        _red = _fadeImage.color.r;
        _green = _fadeImage.color.g;
        _blue = _fadeImage.color.b;
        _currentAlpha = _fadeImage.color.a;

        _fadeSpeed = 1.0f / _fadeDuration;
    }

    private void Update() {
        if (_isCompleted)
            return;

        if (_isFadeIn) {
            if (_currentAlpha < 1.0f) {
                _currentAlpha += _fadeSpeed * Time.deltaTime;
                if (_currentAlpha >= 1.0f) {
                    _isCompleted = true;
                    _currentAlpha = 1.0f;
                }
            }
        } else {
            if (_currentAlpha > 0.0f) {
                _currentAlpha -= _fadeSpeed * Time.deltaTime;
                if (_currentAlpha <= 0.0f) {
                    _isCompleted = true;
                    _currentAlpha = 0.0f;
                }
            }
        }
        _fadeImage.color = new Color(_red, _green, _blue, _currentAlpha);
    }
}

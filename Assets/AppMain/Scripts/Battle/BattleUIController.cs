using System.Collections.Generic;
using ScienceExplosion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour {
    #region
    // private bool _isChangingScene = false;
    private Image _pauseMenuButtonImage = null;

    private float _currentTime = 0;
    private bool _isTimeUp = false;

    private AudioSource _audioSource_BGM = null;

    // C, B, A, S, SS
    private string _currentRank = "C";
    #endregion
    
    #region
    [SerializeField] private GameObject _pauseMenuPanel = null;
    [SerializeField] private Button _pauseMenuButton = null;
    [SerializeField] private List<Sprite> _pauseMenuButtonSprites = new List<Sprite>();

    [SerializeField] private TextMeshProUGUI _timeText = null;
    [SerializeField] private float _timeLimit = 90.0f;

    [SerializeField] private int scorePerPuzzle = 30;

    [SerializeField] private int _maxSliderValue = 10000;
    [SerializeField] private Slider _scoreSlider = null;
    [Header("0...B, 1...A, 2...S, 3...SS")]
    [SerializeField] private List<Image> _ranks = null;
    #endregion

    public bool IsTimeUp { get => _isTimeUp; set => _isTimeUp = value; }
    public string CurrentRank { get => _currentRank; set => _currentRank = value; }

    private void Start() {
        _pauseMenuButtonImage = _pauseMenuButton.GetComponent<Image>();
        _pauseMenuButtonImage.alphaHitTestMinimumThreshold = 1;

        _pauseMenuButton.onClick.AddListener(() => OnPauseMenuButtonClicked());

        _currentTime = _timeLimit;
        _timeText.text = _currentTime.ToString("00");

        _audioSource_BGM = BGM.Instance.GetComponent<AudioSource>();
        Debug.Log(_audioSource_BGM);

        // TODO あんまり考えずにやっちゃてるので書き直すこと
        _scoreSlider.value = GameDirector.Instance.Score;
    }
    
    private void Update() {
        if (!_isTimeUp)
            TimerUpdate();
    }

    private void TimerUpdate() {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0) {
            _currentTime = 0;
            _isTimeUp = true;
        }

        _timeText.text = _currentTime.ToString("00");
    }

    public void AddScore(int puzzleCount, string id) {
        // TODO マジックナンバー変更
        if (id == GameDirector.Instance.PlayerCharacterIndex.ToString())
            GameDirector.Instance.Score += (int)(puzzleCount * scorePerPuzzle * 1.2);
        else
            GameDirector.Instance.Score += (int)puzzleCount * scorePerPuzzle;
        
        // TODO あんまり考えずにやっちゃてるので書き直すこと (加算する感じで)
        var score = GameDirector.Instance.Score;
        if (score <= _maxSliderValue) {
            _scoreSlider.value = score;
            switch (_currentRank) {
                case "C":
                    Debug.Log("Current Rank is C");
                    if (score < 1640)
                        break;
                    var color0 = _ranks[0].color;
                    color0.a = 1.0f;
                    _ranks[0].color = color0;
                    _currentRank = "B";
                    break;
                case "B":
                    Debug.Log("Current Rank is B");
                    if (score < 3280)
                        break;
                    var color1 = _ranks[1].color;
                    color1.a = 1.0f;
                    _ranks[1].color = color1;
                    _currentRank = "A";
                    break;
                case "A":
                    if (score < 6250)
                        break;
                    var color2 = _ranks[2].color;
                    color2.a = 1.0f;
                    _ranks[2].color = color2;
                    _currentRank = "S";
                    break;
                case "S":
                    if (score < 9400)
                        break;
                    var color3 = _ranks[3].color;
                    color3.a = 1.0f;
                    _ranks[3].color = color3;
                    _currentRank = "SS";
                    break;
                default:
                    break;
            }
        } else {
            _scoreSlider.value = _maxSliderValue;
        }
    }

    private void OnPauseMenuButtonClicked() {
        if (_pauseMenuPanel.activeSelf) {
            // TODO 一時停止の方法をtimeScaleを使わないものに変更するかも
            Time.timeScale = 1;
            _audioSource_BGM.UnPause();

            _pauseMenuPanel.SetActive(false);
            _pauseMenuButtonImage.sprite = _pauseMenuButtonSprites[0];
        } else {
            Time.timeScale = 0;
            _audioSource_BGM.Pause();

            _pauseMenuPanel.SetActive(true);
            _pauseMenuButtonImage.sprite = _pauseMenuButtonSprites[1];
        }
    }
}

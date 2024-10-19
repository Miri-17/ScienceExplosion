using System.Collections;
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
    #endregion
    
    #region
    [SerializeField] private GameObject _pauseMenuPanel = null;
    [SerializeField] private Button _pauseMenuButton = null;
    [SerializeField] private List<Sprite> _pauseMenuButtonSprites = new List<Sprite>();

    [SerializeField] private TextMeshProUGUI _timeText = null;
    [SerializeField] private float _timeLimit = 90.0f;

    [SerializeField] private int scorePerPuzzle = 30;
    #endregion

    public bool IsTimeUp { get => _isTimeUp; set => _isTimeUp = value; }

    private void Start() {
        _pauseMenuButtonImage = _pauseMenuButton.GetComponent<Image>();
        _pauseMenuButtonImage.alphaHitTestMinimumThreshold = 1;

        _pauseMenuButton.onClick.AddListener(() => OnPauseMenuButtonClicked());

        _currentTime = _timeLimit;
        _timeText.text = _currentTime.ToString("00");

        _audioSource_BGM = BGM.Instance.GetComponent<AudioSource>();
        Debug.Log(_audioSource_BGM);
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

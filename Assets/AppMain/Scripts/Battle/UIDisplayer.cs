using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIDisplayer : MonoBehaviour {
    private float _currentTime;
    // true <= 初期値, false <= timeが0
    // private bool _isPlaying = true;

    // private int _previousScore = 0;
    // private bool _isCountUp;
    // カウントアップアニメーション
    private Sequence _sequence;
    
    [SerializeField] private float timeLimit = 90.0f;
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI _scoreText;
    // 1つのパズルに対するスコア
    [SerializeField] private int scorePerPuzzle = 30;
    // [SerializeField] private BattleController _battleController;

    private void Start() {
        _currentTime = timeLimit;
        _scoreText.text  = "0";
    }

    private void Update() {
        // if (_battleController.IsBattling) {
        //     TimerUpdate();
        // }
    }

    private void TimerUpdate() {
        // if (_isPlaying) {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0) {
                _currentTime = 0;
                // _isPlaying = false;
                // _battleController.IsBattling = false;
                // _battleController.currentState = BattleController.GameState.BattleFinish;
                // _battleController.PlayTimeline(1);
            }
            GetCurrentFill();
        // }
    }

    private void GetCurrentFill() {
        var fillAmount = _currentTime / timeLimit;
        mask.fillAmount = fillAmount;
    }

    public void AddScore(int puzzleCount, string id) {
        // _previousScore = _scoreText;
        // TODO マジックナンバー変更
        if (id == GameDirector.Instance.PlayerCharacterIndex.ToString())
            GameDirector.Instance.Score += (int)(puzzleCount * scorePerPuzzle * 1.2);
        else
            GameDirector.Instance.Score += (int)puzzleCount * scorePerPuzzle;
        
        _scoreText.text = GameDirector.Instance.Score.ToString();
    }
}

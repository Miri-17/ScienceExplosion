using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIDisplayer : MonoBehaviour {
    private float _currentTime;
    private bool _isPlaying = true;
    
    [SerializeField] private float timeLimit = 90.0f;
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI _scoreText;
    // 1つのパズルに対するスコア
    [SerializeField] private int scorePerPuzzle = 30;

    private void Start() {
        _currentTime = timeLimit;
        _scoreText.text  = "0";
    }

    private void Update() {
        // _currentTime -= Time.deltaTime;
        // if (_currentTime <= 60) {
        //     //
        // }
        // if (_currentTime <= 30) {
        //     //
        // }
        // if (_currentTime <= 0) {
            // SceneManager.LoadScene("Result");
        // }
        // GetCurrentFill();
        TimerUpdate();
    }

    private void TimerUpdate() {
        if (_isPlaying) {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0) {
                _currentTime = 0;
                _isPlaying = false;
            }
            GetCurrentFill();
        }
    }

    private void GetCurrentFill() {
        var fillAmount = _currentTime / timeLimit;
        mask.fillAmount = fillAmount;
    }

    public void AddScore(int puzzleCount, string id) {
        // TODO マジックナンバー変更
        if (id == GameDirector.Instance.PlayerCharacterIndex.ToString())
            GameDirector.Instance.Score += (int)(puzzleCount * scorePerPuzzle * 1.2);
        else
            GameDirector.Instance.Score += (int)puzzleCount * scorePerPuzzle;
        
        _scoreText.text = GameDirector.Instance.Score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private float timeLimit = 90.0f;
    private bool _isPlaying = false;

    public enum GameState {
        BattleStart,    // バトル開始演出中
        BattleMain,     // バトル中
        Win,            // 勝利演出中
        Lose,           // 敗北演出中
    }
    GameState currentState = GameState.BattleStart;

    private void Update() {
        switch (currentState) {
            case GameState.BattleStart:
                BattleStartUpdate();
                break;
            case GameState.BattleMain:
                BattleMainUpdate();
                break;
            case GameState.Win:
                WinUpdate();
                break;
            case GameState.Lose:
                LoseUpdate();
                break;
        }
    }

    private void BattleStartUpdate() {
        // TODO バトル開始演出を行う
        Debug.Log("Buttle Start!");
        _playableDirector.Play();

        // 演出が終わる条件を書く
        currentState = GameState.BattleMain;
    }

    private void BattleMainUpdate() {
        // バトル中の更新を行う
        Debug.Log("In Buttle");

        // 勝利条件
        // if () {
            currentState = GameState.Win;
        // 敗北条件
        // } else if () {
            currentState = GameState.Lose;
        // }
    }

    private void WinUpdate() {
        // TODO 勝利演出を行う
        Debug.Log("You win!");

        // 何かしたら
        // if (Input.anyKeyDown) {
        //     SceneManager.LoadScene("Result");
        // }
    }

    private void LoseUpdate() {
        // TODO 敗北演出を行う
        Debug.Log("You Lose");

        // 何かしたら
        // if (Input.anyKeyDown) {
        //     SceneManager.LoadScene("Result");
        // }
    }
}

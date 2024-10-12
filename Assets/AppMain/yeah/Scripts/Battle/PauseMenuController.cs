using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : AudioMixerController {
    # region
    [SerializeField] private Button _howToPlayButton = null;
    [SerializeField] private Button _retryBattleButton = null;
    [SerializeField] private Button _retreatBattleButton = null;
    #endregion

    // TODO 合ってるか調べる
    private new void Start() {
        base.Start();

        Debug.Log("PauseMenuControllerのStart()が呼ばれた");
        _howToPlayButton.onClick.AddListener(() => OnHowToPlayButtonClicked());
        _retryBattleButton.onClick.AddListener(() => OnRetryBattleButtonClicked());
        _retreatBattleButton.onClick.AddListener(() => OnRetreatBattleButtonClicked());
    }

    private void OnHowToPlayButtonClicked() {
        // TODO 遊び方パネルを表示する処理
    }

    private void OnRetryBattleButtonClicked() {
        // TODO 再挑戦する処理
    }

    private void OnRetreatBattleButtonClicked() {
        // TODO メニュー画面に遷移する処理
    }
}

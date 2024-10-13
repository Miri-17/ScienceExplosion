using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class SettingsController : AudioMixerController {
    // TODO 完全に実装したら消す
    private List<string> _warningTexts = new List<string>() {
        "スタッフロールはまだ見られません。",
        "データ消去は未実装です。",
    };

    # region
    [SerializeField] private Image _settingsBg = null;
    [SerializeField] private Image _hologramBg = null;
    // SettingsのBackgroundをタップするとSettingsが閉じるようにする.
    [SerializeField] private EventTrigger _eventTrigger = null;
    [SerializeField] private GameObject _settingsPanel = null;
    [SerializeField] private Button _showCreditsButton = null;
    [SerializeField] private Button _deleteSaveDataButton = null;
    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private TextMeshProUGUI _warningSentence = null;
    #endregion

    // TODO 合ってるか調べる
    private new void Start() {
        base.Start();

        Debug.Log("SettingsControllerのStart()が呼ばれた");
        _hologramBg.alphaHitTestMinimumThreshold = 1;
        _settingsBg.alphaHitTestMinimumThreshold = 1;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 押した瞬間に実行するようにする.
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => CloseSettings());
        //イベントの設定をEventTriggerに反映
        _eventTrigger.triggers.Add(entry);

        _showCreditsButton.onClick.AddListener(() => OnShowCreditsButtonClicked());
        _deleteSaveDataButton.onClick.AddListener(() => OnDeleteSaveDataButtonClicked());
        
        _settingsPanel.SetActive(false);

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
    }

    private void CloseSettings() {
        if (!_settingsPanel.activeSelf) return;

        Debug.Log("CloseSettings");
        _settingsPanel.SetActive(false);
    }

    private void OnShowCreditsButtonClicked() {
        // Creditsに遷移する処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    private void OnDeleteSaveDataButtonClicked() {
        // セーブデータを消去する処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        _warningSentence.text = _warningTexts[1];
        _notYetInstalledPanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using ScienceExplosion;

public class BattleController : MonoBehaviour {
    private bool _isPauseCutscene = false;
    private bool _isBattling = false;
    // FIXME 上の変数となんか名前が被ってるので変えたい
    // private bool _isFinishBattle = false;

    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    // [SerializeField] private GameObject _BlockPanel = null;
    [SerializeField] private EventTrigger _eventTrigger;

    public bool IsBattling { get => _isBattling; set => _isBattling = value; }
    // public bool IsFinishBattle { get => _isFinishBattle; set => _isFinishBattle = value; }

    private void Start() {
        BGM.Instance.AudioSource.Stop();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        // 押した瞬間に実行するようにする.
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => ResumeCutscene());
        //イベントの設定をEventTriggerに反映
        _eventTrigger.triggers.Add(entry);

        PlayTimeline(0);
    }

    // private void Update() {
    //     if (_isFinishBattle) {
    //         PlayTimeline(1);
    //     }
    // }

    // private void PlayTimeline(int index) {
    public void PlayTimeline(int index) {
        if (index < 0 || index >= _timelineAssets.Count)
            return;
        
        _playableDirector.Play(_timelineAssets[index]);
    }

    public void PauseCutscene() {
        _isPauseCutscene = true;
        _playableDirector.Pause();
    }

    private void ResumeCutscene() {
    // public void ResumeCutscene() {
        if (!_isPauseCutscene) return;
        
        _playableDirector.Resume();
    }

    public void StartMusic() {
        BGM.Instance.AudioSource.Play();
    }

    public void StartTimer() {
        _isBattling = true;
    }
}

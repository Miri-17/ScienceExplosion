using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour {
    private bool _isChangingScene = false;
    private CancellationToken _token = default;

    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private BattleUIController _battleUIController = null;

    // private bool _isPauseCutscene = false;
    // false <= 初期値, true <= タイマースタート時
    // private bool _isBattling = false;
    // private bool _isInFinishDirection = false;
    // private bool _isSceneTransitioning = false;
    // FIXME 上の変数となんか名前が被ってるので変えたい
    // private bool _isFinishBattle = false;

    // [SerializeField] private PlayableDirector _playableDirector = null;
    // [SerializeField] private List<TimelineAsset> _timelineAssets = null;
    // [SerializeField] private GameObject _BlockPanel = null;
    // [SerializeField] private EventTrigger _eventTrigger;

    // public bool IsBattling { get => _isBattling; set => _isBattling = value; }
    // public bool IsFinishBattle { get => _isFinishBattle; set => _isFinishBattle = value; }

    // public enum GameState {
        // BattleStart,    // バトル開始演出中
        // BattleMain,     // バトル中
        // Win,            // 勝利演出中
        // Lose,           // 敗北演出中
        // BattleFinish,
    // }
    // GameState currentState = GameState.BattleStart;
    // public GameState currentState = GameState.BattleMain;

    // private void Start() {
    //     BGM.Instance.AudioSource.Stop();
    //     EventTrigger.Entry entry = new EventTrigger.Entry();
    //     // 押した瞬間に実行するようにする.
    //     entry.eventID = EventTriggerType.PointerDown;
    //     entry.callback.AddListener((x) => ResumeCutscene());
    //     //イベントの設定をEventTriggerに反映
    //     _eventTrigger.triggers.Add(entry);

    //     PlayTimeline(0);
    // }

    // private void Update() {
    //     if (!_isInFinishDirection && currentState == GameState.BattleFinish) {
    //         _isInFinishDirection = true;
    //         PlayTimeline(1);
    //     }
    // }
    private void Update() {
        if (_battleUIController.IsTimeUp) {
            if (_isChangingScene) return;

            _isChangingScene = true;

            GameDirector.Instance.Rank = _battleUIController.CurrentRank;
            
            // TODO durationの変更
            GoNextSceneAsync(0, "Result", false).Forget();
        }
    }

    // public void PlayTimeline(int index) {
    //     Debug.Log(index);
    //     if (index < 0 || index >= _timelineAssets.Count)
    //         return;
        
    //     // TODO 
    //     _playableDirector.initialTime = 0;
    //     _playableDirector.Play(_timelineAssets[index]);
    // }

    // public void PauseCutscene() {
    //     _isPauseCutscene = true;
    //     _playableDirector.Pause();
    // }

    // private void ResumeCutscene() {
    //     if (!_isPauseCutscene) return;
        
    //     // _playableDirector.Resume();
    //     if (!_isSceneTransitioning && _isInFinishDirection) {
    //         _isSceneTransitioning = true;
    //         StartCoroutine(GoToNextScene((float)(_timelineAssets[1].duration - _playableDirector.time)));
    //     } else {
    //         _playableDirector.Resume();
    //     }
    // }

    // private IEnumerator GoToNextScene(float duration) {
    //     _playableDirector.Resume();

    //     yield return new WaitForSeconds(duration);

    //     SceneManager.LoadScene("Result");
    // }

    // public void StartMusic() {
    //     BGM.Instance.AudioSource.Play();
    //     // _playableDirector.Resume();
    //     // _playableDirector.Play();
    // }

    // public void StartTimer() {
    //     _isBattling = true;
    // }

    // TODO GoNextSceneAsyncシリーズはこれを中心にオーバーライドさせること
    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時
        if (isShowLoadingPanel && _loadingPanel != null) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

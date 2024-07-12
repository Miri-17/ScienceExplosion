using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

public class MenuView : ViewBase {
    [SerializeField] private UITransition _buttonTransition = null;

    private void Start() {
        // FIXME Debug用
        var eventTrigger = this.GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener(x => {
            // _text.enabled = false;
            Scene.ChangeScene("Title").Forget();
        });
        eventTrigger.triggers.Add(entry);
    }
    
    public override async void OnViewOpened() {
        base.OnViewOpened();
        await Open();
    }

    public override void OnViewClosed() {
        base.OnViewClosed();
    }

    // ビューを開いている途中は、ボタンの処理を待機する
    private async UniTask Open() {
        _buttonTransition.CanvasGroup.alpha = 0;
        _buttonTransition.gameObject.SetActive(true);

        await _buttonTransition.TransitionInWait();
    }

    // ボタンの処理を先に実行し、ビューを閉じる
    private async UniTask Close() {
        await _buttonTransition.TransitionOutWait();
        _buttonTransition.gameObject.SetActive(false);
    }
}

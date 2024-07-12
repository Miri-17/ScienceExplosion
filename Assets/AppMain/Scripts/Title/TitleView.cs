using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class TitleView : ViewBase {
    [SerializeField] private Text _text;

    private void Start() {
        var eventTrigger = this.GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener(x => {
            _text.enabled = false;
            Scene.ChangeScene("Menu").Forget();
        });
        eventTrigger.triggers.Add(entry);
    }
    
    public override void OnViewOpened() {
        base.OnViewOpened();
    }

    public override void OnViewClosed() {
        base.OnViewClosed();
    }
}

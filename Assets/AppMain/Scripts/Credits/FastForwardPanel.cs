using UnityEngine;
using UnityEngine.EventSystems;

public class FastForwardPanel : MonoBehaviour {
    // private float _startTime = 0;
    // private bool _isTapping = false;

    // [SerializeField, Header("ロングタップ成立までの時間")] private float _time = 2.0f;

    // private void Start() {
    //     AddEventSample(EventTriggerType.PointerDown);
    //     AddEventSample(EventTriggerType.PointerUp);
    //     AddEventSample(EventTriggerType.PointerExit);
    // }

    // private void FixedUpdate() {
    //     if (_isTapping) {
    //         float leftTime = _time - (Time.time - _startTime);

    //         // ロングタップの成立
    //         if (leftTime < 0) {
    //             // 初期化しておく
    //             _isTapping = false;
    //             _startTime = 0;

    //             // ロングタップの目的の処理を実行
    //             OnLongTap();
    //         }
    //     }
    // }

    // public void AddEventSample(EventTriggerType eventTriggerType) {
    //     EventTrigger currentTrigger = this.GetComponent<EventTrigger>();

    //     EventTrigger.Entry entry = new EventTrigger.Entry();
    //     entry.eventID = eventTriggerType;
    //     switch (eventTriggerType) {
    //         case EventTriggerType.PointerDown:
    //             Debug.Log("PointerDown");
    //             entry.callback.AddListener((x) => PointEnter());
    //             break;
    //         case EventTriggerType.PointerUp:
    //             Debug.Log("PointerUp");
    //             entry.callback.AddListener((x) => PointExit());
    //             break;
    //         case EventTriggerType.PointerExit:
    //             Debug.Log("PointerExit");
    //             entry.callback.AddListener((x) => PointExit());
    //             break;
    //         default:
    //             break;
    //     }
    //     currentTrigger.triggers.Add(entry); 
    // }

    // private void PointEnter() {
    //     Debug.Log("enter");
    //     _isTapping = true;
    //     _startTime = Time.time;
    // }

    // private void PointExit() {
    //     if (_isTapping) {
    //         Debug.Log("exit");
    //         _isTapping = false;
    //         // Time.timeScale = 1.0f;
    //     }
    // }

    // private void OnLongTap() {
    //     Debug.Log("long tap");
    //     Time.timeScale = 2.0f;
    // }
}

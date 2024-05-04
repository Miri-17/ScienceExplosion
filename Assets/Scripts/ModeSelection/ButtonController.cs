using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModeSelection {
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private EventTrigger eventTrigger;

        private void Start() {
            // eventTrigger = settingsPanel.GetComponent<EventTrigger>();
            //イベントの設定に入る
            EventTrigger.Entry entry = new EventTrigger.Entry();
            //PointerDown(押した瞬間に実行する)イベントタイプを設定
            entry.eventID = EventTriggerType.PointerDown;
            //関数を設定
            entry.callback.AddListener((x) =>
            {
                Trigger();
            });
            //イベントの設定をEventTriggerに反映
            eventTrigger.triggers.Add(entry);
        }

        public void OnButton4Clicked() {
            settingsPanel.SetActive(true);
        }

        public void Trigger()
        {
            Debug.Log("Trigger");
            settingsPanel.SetActive(false);
        }
    }
}

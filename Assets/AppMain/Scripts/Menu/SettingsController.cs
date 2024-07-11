using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ScienceExplosion.Menu {
    public class SettingsController : MonoBehaviour {
        // SettingsのBackgroundをタップするとSettingsが閉じるようにする.
        [SerializeField] private EventTrigger _eventTrigger;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Slider _audioSlider_BGM;
        [SerializeField] private Slider _audioSlider_SE;

        private AudioSource _audioSource_BGM;
        private AudioSource _audioSource_SE;

        private void Start() {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            // 押した瞬間に実行するようにする.
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((x) => CloseSettings());
            //イベントの設定をEventTriggerに反映
            _eventTrigger.triggers.Add(entry);

            _audioSource_BGM = BGM.Instance.GetComponent<AudioSource>();
            _audioSource_SE = SE.Instance.GetComponent<AudioSource>();
            _audioSlider_BGM.value = _audioSource_BGM.volume;
            _audioSlider_SE.value = _audioSource_SE.volume;

            _audioSlider_BGM.onValueChanged.AddListener((x) => ChangeBGMVolume());
            _audioSlider_SE.onValueChanged.AddListener((x) => ChangeSEVolume());
            
            _settingsPanel.SetActive(false);
        }

        private void CloseSettings() {
            if (!_settingsPanel.activeSelf) return;

            Debug.Log("CloseSettings");
            _settingsPanel.SetActive(false);
        }

        public void ChangeBGMVolume() {
            _audioSource_BGM.volume = _audioSlider_BGM.value;
        }
        
        public void ChangeSEVolume() {
            _audioSource_SE.volume = _audioSlider_SE.value;
        }
    }
}

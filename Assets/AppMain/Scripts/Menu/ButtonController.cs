using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Buttonをリストにまとめるかも
namespace ScienceExplosion.Menu {
    public class ButtonController : MonoBehaviour {
        #region
        // Buttonをリストにまとめるか？
        [SerializeField] private Button _charactersButton;
        [SerializeField] private Button _cutscenesButton;
        [SerializeField] private Button _chargeButton;
        [SerializeField] private Button _soundTrackButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _charactersButton.onClick.AddListener(() => OnCharactersButtonClicked());
            _cutscenesButton.onClick.AddListener(() => OnCutscenesButtonClicked());
            _chargeButton.onClick.AddListener(() => OnChargeButtonClicked());
            _soundTrackButton.onClick.AddListener(() => OnSoundTrackButtonClicked());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked());
        }

        private void OnCharactersButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoToNextScene("Characters"));
        }

        private void OnCutscenesButtonClicked() {
            if (_isChangeScene) return;
            

            _isChangeScene = true;
            StartCoroutine(GoToNextScene("Cutscenes"));
        }

        private void OnChargeButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoToNextScene("ModeSelection"));
        }

        private void OnSoundTrackButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoToNextScene("SoundTrack"));
        }

        private void OnSettingsButtonClicked() {
            if (_isChangeScene || _settingsPanel.activeSelf) return;

            _settingsPanel.SetActive(true);
        }

        private IEnumerator GoToNextScene(string nextSceneName) {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(nextSceneName);
        }
    }
}

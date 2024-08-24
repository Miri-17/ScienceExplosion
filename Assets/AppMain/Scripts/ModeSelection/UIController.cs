using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace ScienceExplosion.ModeSelection {
    public class UIController : MonoBehaviour
    {
        #region 
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _playAloneButton;
        [SerializeField] private Button _playTwoButton; // TODO ネットワークのこと学んだら変更あり
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            _playAloneButton.onClick.AddListener(() => OnPlayAloneButtonClicked());
            _playTwoButton.onClick.AddListener(() => OnPlayTwoButtonClicked());
        }

        private void OnBackButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(ChangeScene("Menu"));
        }

        private void OnPlayAloneButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            // StartCoroutine(ChangeScene("CharacterSelection"));
            StartCoroutine(ChangeScene("CharacterSelection"));
        }

        private void OnPlayTwoButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            // StartCoroutine(ChangeScene("Netcode"));
            StartCoroutine(ChangeScene("Netcode"));
        }

        private IEnumerator ChangeScene(string sceneName) {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}

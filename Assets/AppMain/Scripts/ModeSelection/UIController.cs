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
        [SerializeField] private Button _startPlayButton; // TODO ネットワークのこと学んだら変更あり
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            _startPlayButton.onClick.AddListener(() => OnStartPlayButtonClicked());
        }

        private void OnBackButtonClicked() {
             if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(ChangeScene("Menu"));
        }

        private void OnStartPlayButtonClicked() {
             if (_isChangeScene) return;

            _isChangeScene = true;
            // StartCoroutine(ChangeScene("CharacterSelection"));
            StartCoroutine(ChangeScene("Netcode"));
        }

        private IEnumerator ChangeScene(string sceneName) {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}

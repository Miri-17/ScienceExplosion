using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScienceExplosion.Characters {
    public class UIController : MonoBehaviour {
        #region 
        [SerializeField] private Button _backButton;
        [SerializeField] private Button[] _images;
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            foreach (Button image in _images) {
                image.onClick.AddListener(() => PushImage());
            }
        }

        private void OnBackButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(ChangeScene("Menu"));
        }

        private void PushImage() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(ChangeScene("Character"));
        }

        private IEnumerator ChangeScene(string sceneName) {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}

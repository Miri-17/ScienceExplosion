using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScienceExplosion.Characters {
    public class UIController : MonoBehaviour {
        #region 
        [SerializeField] private Button _backButton;
        [SerializeField] private List<Button> _buttons;
        // [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            for (var i = 0; i < _buttons.Count; i++) {
                var index = i;
                _buttons[i].onClick.AddListener(() => PushImage(index));
            }
        }

        private void OnBackButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(ChangeScene("Menu"));
        }

        private void PushImage(int index) {
            if (_isChangeScene) return;

            _isChangeScene = true;
            GameDirector.Instance.CharactersFirstIndex = index;
            StartCoroutine(ChangeScene("Character"));
        }

        private IEnumerator ChangeScene(string sceneName) {
            // _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}

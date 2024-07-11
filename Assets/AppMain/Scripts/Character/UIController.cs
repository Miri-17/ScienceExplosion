using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// TODO Complete to make character database
namespace ScienceExplosion.Character {
    public class UIController : MonoBehaviour
    {
        #region 
        // add
        [SerializeField] private Button[] _characterSelectionButton;
        [SerializeField] private Text _nameText;

        [SerializeField] private Button _backButton;
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private string[] _characterNames = new string[9]{ "Dr.P", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Black" };
        private bool _isChangeScene = false;

        private void Start() {
            // add
            for (int i = 0; i < _characterSelectionButton.Length; i++) {
                int characterIndex = i;
                _characterSelectionButton[i].onClick.AddListener(() => OnCharacterSelectionButtonClicked(characterIndex));
            }

            _nameText.text = _characterNames[0];
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }
        
        private void OnCharacterSelectionButtonClicked(int index) {
            // Debug.Log("push" + index);
            _nameText.text = _characterNames[index];
        }

        private void OnBackButtonClicked() {
             if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        private IEnumerator GoBackToScene() {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene("Characters");
        }
    }
}

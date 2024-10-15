using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundTrackUIController : MonoBehaviour {
    #region 
    [SerializeField] private Button _backButton;
    // [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime = 1.0f;
    #endregion

    private bool _isChangeScene = false;

    private void Start() {
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
    }

    private void OnBackButtonClicked() {
            if (_isChangeScene) return;

        _isChangeScene = true;
        StartCoroutine(GoBackToScene());
    }

    private IEnumerator GoBackToScene() {
        // _transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene("Menu");
    }
}

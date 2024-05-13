using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

// 警告文を出すように変更
namespace ScienceExplosion.CharacterSelection {
    public class UIController : MonoBehaviour {
        #region
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject mask;
        [SerializeField] private GameObject maskableObject;
        #endregion

        private RectTransform maskRectTransform;
        private bool _isChangeScene = false;

        private void Start() {
            _exitButton.onClick.AddListener(() => OnExitButtonClicked());
            _startButton.onClick.AddListener(() => OnStartButtonClicked());

            maskRectTransform = mask.GetComponent<RectTransform>();
        }

        private void OnExitButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        // 警告文を出すように変更
        private void OnStartButtonClicked() {
            if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoToBattle());
        }

        private IEnumerator GoBackToScene() {

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene("ModeSelection");
        }

        private IEnumerator GoToBattle() {
            maskableObject.transform.SetParent(mask.transform, false);
            maskRectTransform.DOSizeDelta(new Vector2(0f, 0f), 0.5f)
                .SetEase(Ease.InBack)
                .SetLink(maskRectTransform.gameObject);

            yield return new WaitForSeconds(0.5f);
            
            Debug.Log("Go to Battle");
            SceneManager.LoadScene("Battle");
        }
    }    
}

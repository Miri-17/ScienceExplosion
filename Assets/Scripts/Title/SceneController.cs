using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Title {
    public class SceneController : MonoBehaviour {
        #region
        [SerializeField] private SkeletonGraphic[] _skeletonGraphic;
        [SerializeField] private Text _text;
        [SerializeField] private Image _whiteImage;
        [SerializeField] private GameObject _explosionImage;
        #endregion

        private bool _isTapped = false;

        private void Update() {
            if (!_isTapped && Input.GetMouseButtonDown(0)) {
                _isTapped = true;
                StartCoroutine(GoToNextScene());
            }
        }

        private IEnumerator GoToNextScene() {
            _whiteImage.enabled = true;
            _text.enabled = false;
            _skeletonGraphic[0].color = new Color(100f, 100f, 100f);

            yield return new WaitForSeconds(0.1f);

            _whiteImage.enabled = false;
            _explosionImage.SetActive(true);

            yield return new WaitForSeconds(0.4f);
            
            Debug.Log("Go to ModeSelection");
            SceneManager.LoadScene("ModeSelection");
        }
    }
}

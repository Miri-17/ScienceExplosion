using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace ScienceExplosion.Title {
    public class SceneController : MonoBehaviour
    {
        #region
        [SerializeField] private Text _text;
        [SerializeField] private Image _whiteImage;
        [SerializeField] private Image _fadeImage;
        #endregion

        public IEnumerator GoToNextScene() {
            _whiteImage.enabled = true;
            _text.enabled = false;

            yield return new WaitForSeconds(0.2f);

            _whiteImage.enabled = false;
            _fadeImage.DOFade(1f, 0.8f);

            yield return new WaitForSeconds(0.8f);
            
            Debug.Log("Go to Menu");
            SceneManager.LoadScene("Menu");
        }
    }
}

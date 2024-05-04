using System.Collections;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Title {
    public class SceneController : MonoBehaviour {
        [SerializeField] private SkeletonGraphic _skeletonGraphic;
        [SerializeField] private Text text;
        [SerializeField] private Image whiteImage;
        [SerializeField] private GameObject explosionImage;

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                StartCoroutine(GoToNextScene());
            }
        }

        private IEnumerator GoToNextScene() {
            whiteImage.enabled = true;
            text.enabled = false;
            _skeletonGraphic.color = new Color(100f, 100f, 100f);

            yield return new WaitForSeconds(0.1f);

            whiteImage.enabled = false;
            explosionImage.SetActive(true);

            yield return new WaitForSeconds(0.4f);
            
            Debug.Log("Go to ModeSelection");
            SceneManager.LoadScene("ModeSelection");
        }
    }
}

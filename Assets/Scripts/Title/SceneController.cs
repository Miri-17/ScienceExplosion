using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace ScienceExplosion.Title {
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public IEnumerator GoToNextScene() {
            _text.enabled = false;
            
            yield return new WaitForSeconds(1.0f);
            
            Debug.Log("Go to Menu");
            SceneManager.LoadScene("Menu");
        }
    }
}

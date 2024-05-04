using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace ModeSelection {
    public class SceneController : MonoBehaviour {
        [SerializeField] private GameObject mask;
        [SerializeField] private GameObject maskableObject;

        private RectTransform maskRectTransform;

        private void Start() {
            maskRectTransform = mask.GetComponent<RectTransform>();
        }

        private void Update() {
            // if (Input.GetMouseButtonDown(0)) {
            //     StartCoroutine(GoToNextScene());
            // }
        }

        // private IEnumerator GoToNextScene() {
        //     maskableObject.transform.SetParent(mask.transform, false);
        //     maskRectTransform.DOSizeDelta(new Vector2(0f, 0f), 0.5f)
        //         .SetEase(Ease.InBack)
        //         .SetLink(maskRectTransform.gameObject);

        //     yield return new WaitForSeconds(0.5f);
            
        //     Debug.Log("Go to Title");
        //     SceneManager.LoadScene("Title");
        // }
    }
}

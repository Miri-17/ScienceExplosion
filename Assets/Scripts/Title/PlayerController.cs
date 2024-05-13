using UnityEngine;
using UnityEngine.InputSystem;

// InputSystemに対応できていないところがあります

namespace ScienceExplosion.Title {
    public class PlayerController : MonoBehaviour {
        #region
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private TapEffectsController _tapEffectsController;
        #endregion

        private AudioSource _audioSource_SE;
        private AudioClip _audioClip_SE;
        private bool _isChangeScene = false;

        private void Start() {
            _audioSource_SE = SE.Instance.GetComponent<AudioSource>();
            _audioClip_SE = SE.Instance.audioClips[0];
        }

        // 以下、InputSystemに対応させること.
        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                _audioSource_SE.PlayOneShot(_audioClip_SE);
                _tapEffectsController.SpawnEffects();
            }
        }

        public void OnRelease(InputAction.CallbackContext context) {
            if (!context.performed) return;
            Debug.Log("Release!");

            if (_isChangeScene) return;
            _isChangeScene = true;
            Debug.Log("GoToNextScene");
            StartCoroutine(_sceneController.GoToNextScene());
        }
    }
}

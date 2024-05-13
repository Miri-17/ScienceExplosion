using UnityEngine;

// 他と同じになったら、それに統一する.
namespace ScienceExplosion.CharacterSelection {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private TapEffectsController _tapEffectsController;

        private AudioSource _audioSource_SE;
        private AudioClip _audioClip_SE;

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
    }
}

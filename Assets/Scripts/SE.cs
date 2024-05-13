using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScienceExplosion {
    public class SE : MonoBehaviour {
        [HideInInspector] public static SE Instance { get; private set; }

        public AudioClip[] audioClips;

        private AudioSource _audioSource;

        private void Awake() {
            // SEがすでにロードされていたら、自分自身を破棄して終了する.
            if (Instance != null) {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start() {
            _audioSource = this.GetComponent<AudioSource>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScienceExplosion {
    public class BGM : MonoBehaviour {
        [HideInInspector] public static BGM Instance { get; private set; }

        [SerializeField] private AudioClip[] _audioClips;

        private AudioSource _audioSource;

        private void Awake() {
            // BGMがすでにロードされていたら、自分自身を破棄して終了する.
            if (Instance != null) {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start() {
            _audioSource = this.GetComponent<AudioSource>();

            // 本当は↓だけでもいいとは思う. デバッグ用
            // audioSource.clip = audioClips[0];
            switch (SceneManager.GetActiveScene().name) {
                case "Title":
                    _audioSource.clip = _audioClips[0];
                    break;
                case "Menu":
                    _audioSource.clip = _audioClips[1];
                    break;
                default:
                    break;
            }

            _audioSource.Play();

            // シーンが切り替わった時に呼ばれるメソッドを登録する.
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        private void ChangedActiveScene (Scene thisScene, Scene nextScene) {
            Debug.Log(nextScene.name);

            switch (nextScene.name) {
                case "Title":
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[0];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                case "Menu":
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[1];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                default:
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScienceExplosion {
    public class BGM : MonoBehaviour {
        [HideInInspector] public static BGM Instance { get; private set; }

        [SerializeField] private List<AudioClip> _audioClips;

        private AudioSource _audioSource = null;
        private int _titleBGMIndex = 0;
        private int _menuBGMIndex = 1;

        public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }
        public List<AudioClip> AudioClips { get => _audioClips; set => _audioClips = value; }

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
                    _audioSource.clip = _audioClips[_titleBGMIndex];
                    break;
                case "Menu":
                case "Characters":
                case "Character":
                case "Cutscenes":
                case "ModeSelection":
                case "CharacterSelection":
                    _audioSource.clip = _audioClips[_menuBGMIndex];
                    break;
                case "SoundTrack":
                default:
                    break;
            }

            _audioSource.Play();

            // シーンが切り替わった時に呼ばれるメソッドを登録する.
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        private void ChangedActiveScene (Scene thisScene, Scene nextScene) {
            Debug.Log("This Scene is " + thisScene.name);
            Debug.Log("Next Scene is " + nextScene.name);

            switch (nextScene.name) {
                case "Title":
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[0];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                case "Menu":
                    if (_audioSource.clip == _audioClips[1])
                        break;
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[1];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                case "SoundTrack":
                    _audioSource.Stop();
                    break;
                default:
                    break;
            }
        }
    }
}

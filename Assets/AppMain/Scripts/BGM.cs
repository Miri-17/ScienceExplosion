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
        private int _battleBGMIndex = 2;

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

            #region デバッグ用
            switch (SceneManager.GetActiveScene().name) {
                case "Title":
                    _audioSource.clip = _audioClips[_titleBGMIndex];
                    break;
                case "Menu":
                case "Characters":
                case "Map":
                case "Cutscenes":
                case "ModeSelection":
                case "PlayerSelection":
                case "EnemySelection":
                case "Result":
                case "Credits":
                    _audioSource.clip = _audioClips[_menuBGMIndex];
                    break;
                case "Audio":
                    _audioSource.clip = _audioClips[3];
                    break;
                case "Battle":
                    _audioSource.clip = _audioClips[_battleBGMIndex + GameDirector.Instance.EnemyCharacterIndex];
                    break;
                case "SoundTrack":
                default:
                    break;
            }
            #endregion
            // TODO 上のデバッグ用の部分をこちらに変更すると, コードが短くなる.
            // audioSource.clip = audioClips[0];

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
                    _audioSource.clip = _audioClips[_titleBGMIndex];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                case "Menu":
                case "Result":
                    // 後ろの条件だけだと、曲選択で何の曲も選択せず戻ると無音.
                    if (_audioSource.isPlaying && _audioSource.clip == _audioClips[_menuBGMIndex])
                        break;
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[_menuBGMIndex];
                    Debug.Log("audioSource.clip: " + _audioSource.clip);
                    _audioSource.Play();
                    break;
                case "Battle":
                    if (_audioSource.isPlaying && _audioSource.clip == _audioClips[_battleBGMIndex])
                        break;
                    _audioSource.Stop();
                    _audioSource.clip = _audioClips[_battleBGMIndex + GameDirector.Instance.EnemyCharacterIndex];
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

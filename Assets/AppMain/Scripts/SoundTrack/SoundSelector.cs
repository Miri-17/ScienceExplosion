using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScienceExplosion;
using UnityEngine;
using UnityEngine.UI;

public class SoundSelector : MonoBehaviour {
    // [SerializeField] private CharacterDatabase _characterDatabase;
    [SerializeField] private List<Button> _soundButtons = null;

    [SerializeField] private Button _selectButton = null;

    [SerializeField] private AudioSource _soundTrackAudioSource;

    private List<bool> _isPlayingMusic = Enumerable.Repeat(false, 9).ToList();

    private void Start () {
        for (var i = 0; i < _soundButtons.Count; i++) {
            var index = i;
            _soundButtons[i].onClick.AddListener(() => ChangeMusic(index));
        }
        _selectButton.onClick.AddListener(() => SetHomeMusic());
    }

    // TODO nowIndexとindexがあまり良い名前とはいえないので変えること
    private void ChangeMusic(int index) {
        // var character = _characterDatabase.GetCharacter(index);
        var nowIndex = _isPlayingMusic.FindIndex(n => n == true);

        // 何も流れていない時は
        if (nowIndex == -1) {
            _isPlayingMusic[index] = true;
            // BGM.Instance.AudioSource.clip = character.UniqueAudioClip;
            // _soundTrackAudioSource.clip = character.UniqueAudioClip;
            // BGM.Instance.AudioSource.Play();
            _soundTrackAudioSource.Play();
            return;
        }

        _isPlayingMusic[nowIndex] = false;
        // BGM.Instance.AudioSource.Stop();
        _soundTrackAudioSource.Stop();
        // 今流れている曲と同じボタンを押した時は再生停止
        // そうでない時は別の曲を再生
        if (nowIndex != index) {
            _isPlayingMusic[index] = true;
            // BGM.Instance.AudioSource.Stop();
            _soundTrackAudioSource.Stop();
            // BGM.Instance.AudioSource.clip = character.UniqueAudioClip;
            // _soundTrackAudioSource.clip = character.UniqueAudioClip;
            // BGM.Instance.AudioSource.Play();
            _soundTrackAudioSource.Play();
        }
    }

    private void SetHomeMusic() {
        var nowIndex = _isPlayingMusic.FindIndex(n => n == true);
        if (nowIndex == -1)
            return;
        
        // var character = _characterDatabase.GetCharacter(nowIndex);
        // BGM.Instance.AudioClips[1] = character.UniqueAudioClip;
    }
}

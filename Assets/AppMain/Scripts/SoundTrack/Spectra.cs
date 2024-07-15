using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

namespace ScienceExplosion.SoundTrack {
    public class Spectra : MonoBehaviour {
        // [SerializeField] AudioSpectrum _audioSpectrum;
        // [SerializeField] private RectTransform[] _masks;
        // [Header("スペクトラムの高さ倍率"), SerializeField] private float _scale;
        // [SerializeField] GameObject[] _maskableObjects;

        // // [SerializeField] AudioSpectrumManager _audioSpectrumManager;


        // private void Start() {
        //     for (int i = 0; i < _maskableObjects.Length && i < _masks.Length; i++) {
        //         _maskableObjects[i].transform.SetParent(_masks[i].transform, false);
        //     }
        // }

        // private void Update() {
        //     int i = 0;
        //     foreach (RectTransform mask in _masks) {
        //         float y = _audioSpectrum.Levels[i] * _scale;
        //         // float y = _audioSpectrumManager.Levels[i] * _scale;
        //         mask.DOSizeDelta(new Vector2(100.0f, y), 0.0f)
        //             .SetLink(mask.gameObject);
        //         i++;
        //     }
        // }
    }
}

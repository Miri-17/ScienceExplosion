using System;
using System.Collections;
using System.Collections.Generic;
using ScienceExplosion;
using UnityEngine;
using UnityEngine.UI;

namespace ModeSelection {
    public class SettingsController : MonoBehaviour {
        [SerializeField] private Slider audioSlider;

        private AudioSource audioSource;

        private void Start() {
            audioSource = BGM.Instance.GetComponent<AudioSource>();
            audioSlider.value = audioSource.volume;
        }

        public void ChangeBGMVolume() {
            audioSource.volume = audioSlider.value;
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// SettingsControllerとPauseMenuControllerに継承されるクラス.
/// </summary>
public class AudioMixerController : MonoBehaviour {
    #region Serialized Fields
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private Slider _masterSlider = null;
    [SerializeField] private Slider _bgmSlider = null;
    [SerializeField] private Slider _seSlider = null;
    #endregion

    protected void Start() {
        Debug.Log("AudioMixerControllerのStart()が呼ばれた");
        _masterSlider.onValueChanged.AddListener((x) => ChangeMasterVolume(x));
        _bgmSlider.onValueChanged.AddListener((x) => ChangeBGMVolume(x));
        _seSlider.onValueChanged.AddListener((x) => ChangeSEVolume(x));

        _masterSlider.value = GameDirector.Instance.MasterSliderValue;
        _bgmSlider.value = GameDirector.Instance.BGMSliderValue;
        _seSlider.value = GameDirector.Instance.SESliderValue;
    }

    public void ChangeMasterVolume(float value) {
        value /= 10;
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        _audioMixer.SetFloat("VolumeMaster", volume);
        Debug.Log($"VolumeMaster: {volume}");
        GameDirector.Instance.MasterSliderValue = _masterSlider.value;
        Debug.Log("Master: " + _masterSlider.value);
    }
    
    public void ChangeBGMVolume(float value) {
        value /= 10;
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        _audioMixer.SetFloat("VolumeBGM", volume);
        Debug.Log($"VolumeBGM: {volume}");
        GameDirector.Instance.BGMSliderValue = _bgmSlider.value;
        Debug.Log("BGM: " + _bgmSlider.value);
    }
    
    public void ChangeSEVolume(float value) {
        value /= 10;
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        _audioMixer.SetFloat("VolumeSE", volume);
        Debug.Log($"VolumeSE: {volume}");
        GameDirector.Instance.SESliderValue = _seSlider.value;
        Debug.Log("SE: " + _seSlider.value);
    }
}
